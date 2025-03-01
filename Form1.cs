using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Card_Manager.Properties;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using OfficeOpenXml;



namespace Card_Manager
{
    public partial class GiftCardManager : Form
    {
        private Timer countdownTimer;
        private Dictionary<Button, int> countdowns = new Dictionary<Button, int>();
        private readonly ToolTip toolTip = new ToolTip();
        private Dictionary<Button, PictureBox> buttonToPictureBox = new Dictionary<Button, PictureBox>();
        private Dictionary<string, string> originalBalances = new Dictionary<string, string>();
        private Dictionary<Button, string> originalButtonText = new Dictionary<Button, string>();

        public GiftCardManager()
        {
            InitializeComponent();
            InitializeCountdownTimer();
            InitializeButtonPictureMappings();
            InitializeGiftCardFormmater();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Required for EPPlus
        }

        #region LightBox Mapping
        private void InitializeButtonPictureMappings()
        {
            buttonToPictureBox[Timer3M_Button1] = lightBox1;
            buttonToPictureBox[Timer3M_Button2] = lightBox2;
            buttonToPictureBox[Timer3M_Button3] = lightBox3;
            buttonToPictureBox[Timer3M_Button4] = lightBox4;
            buttonToPictureBox[Timer3M_Button5] = lightBox5;
            buttonToPictureBox[Timer3M_Button6] = lightBox6;
            buttonToPictureBox[Timer3M_Button7] = lightBox7;
            buttonToPictureBox[Timer3M_Button8] = lightBox8;
            buttonToPictureBox[Timer3M_Button9] = lightBox9;
            buttonToPictureBox[Timer3M_Button10] = lightBox10;
            buttonToPictureBox[Timer3M_Button11] = lightBox11;
        }
        #endregion

        #region timer functions
        private void InitializeCountdownTimer()
        {
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 second
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void StartCountdown(Button button, int durationInSeconds)
        {
            if (!countdowns.ContainsKey(button))
            {
                countdowns[button] = durationInSeconds;

                // ✅ Store original text only if it's not already stored
                if (!originalButtonText.ContainsKey(button))
                {
                    originalButtonText[button] = button.Text;
                }

                button.Text = FormatTime(durationInSeconds);
                countdownTimer.Start();
            }
        }

        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int secs = seconds % 60;
            return $"{minutes:D2}:{secs:D2}";
        }
        private async void CountdownTimer_Tick(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>(countdowns.Keys);
            List<Button> finishedButtons = new List<Button>();

            foreach (Button button in buttons)
            {
                // ✅ Prevent KeyNotFoundException
                if (!countdowns.ContainsKey(button)) continue;

                int remainingTime = countdowns[button] - 1;

                if (remainingTime <= 0)
                {
                    button.Text = "Time's Up!";
                    finishedButtons.Add(button);

                    // 🔊 Play a notification sound
                    string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "alarm.wav");
                    if (File.Exists(soundPath))
                    {
                        SoundPlayer player = new SoundPlayer(soundPath);
                        player.Play();
                    }

                    // 🟢 Change PictureBox to green
                    if (buttonToPictureBox.TryGetValue(button, out PictureBox picturebox))
                    {
                        picturebox.Image = Resources.green;
                    }

                    // ⏳ Wait for 3 seconds
                    await Task.Delay(3000);

                    // ⏪ Restore button text safely
                    if (originalButtonText.TryGetValue(button, out string originalText))
                    {
                        button.Text = originalText;
                        originalButtonText.Remove(button); // Cleanup after restoring
                    }

                }
                else
                {
                    button.Text = FormatTime(remainingTime);
                    countdowns[button] = remainingTime;
                }
            }

            // 🗑 Remove finished buttons safely
            foreach (Button button in finishedButtons)
            {
                countdowns.Remove(button);
            }

            // 🛑 Stop timer if no active countdowns
            if (countdowns.Count == 0)
            {
                countdownTimer.Stop();
            }
        }
        #endregion

        #region Generate Report Button
        private void button36_Click(object sender, EventArgs e)
        {
            GenerateExcelReport();
        }
        #endregion

        #region Report Generator
        private void GenerateExcelReport()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory; // Get the application's base directory
            string reportsFolder = Path.Combine(appFolder, "reports"); // Create the "reports" folder path

            // Ensure the "reports" folder exists
            if (!Directory.Exists(reportsFolder))
            {
                Directory.CreateDirectory(reportsFolder);
            }

            // Generate unique report number (increment based on existing files)
            int reportNumber = Directory.GetFiles(reportsFolder, "Report(*-*-*).xlsx").Length + 1;

            // Format: Report(Number-Date-Time).xlsx
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string fileName = $"Report({reportNumber})-{timestamp}.xlsx";

            // Define the file path inside the "reports" folder
            string filePath = Path.Combine(reportsFolder, fileName);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                // Headers
                worksheet.Cells[1, 1].Value = "Card";
                worksheet.Cells[1, 2].Value = "Original Balance";
                worksheet.Cells[1, 3].Value = "Balance";
                worksheet.Cells[1, 4].Value = "Used"; // New column

                int row = 2;
                double totalOriginalBalance = 0;
                double totalBalance = 0;
                double totalUsed = 0;

                for (int i = 1; i <= 11; i++)
                {
                    Control[] giftCardInputs = this.Controls.Find($"GiftCardInput{i}", true);
                    Control[] balanceInputs = this.Controls.Find($"BalanceInput{i}", true);

                    if (giftCardInputs.Length > 0 && balanceInputs.Length > 0 &&
                        giftCardInputs[0] is TextBox giftCardBox &&
                        balanceInputs[0] is TextBox balanceBox)
                    {
                        string cardText = giftCardBox.Text;
                        string balanceText = balanceBox.Text;
                        string originalBalanceText = originalBalances.ContainsKey($"BalanceInput{i}") ? originalBalances[$"BalanceInput{i}"] : "0.00";

                        double originalBalance = double.TryParse(originalBalanceText, out double origBal) ? origBal : 0.00;
                        double balance = double.TryParse(balanceText, out double bal) ? bal : 0.00;
                        double used = originalBalance - balance; // Calculate used amount

                        worksheet.Cells[row, 1].Value = cardText;
                        worksheet.Cells[row, 2].Value = originalBalance;
                        worksheet.Cells[row, 3].Value = balance;
                        worksheet.Cells[row, 4].Value = used; // Add Used column

                        // Accumulate totals
                        totalOriginalBalance += originalBalance;
                        totalBalance += balance;
                        totalUsed += used;

                        row++;
                    }
                }

                // Add Sum Row
                worksheet.Cells[row, 1].Value = "Total:";
                worksheet.Cells[row, 2].Value = totalOriginalBalance;
                worksheet.Cells[row, 3].Value = totalBalance;
                worksheet.Cells[row, 4].Value = totalUsed; // Total used amount
                worksheet.Cells[row, 1, row, 4].Style.Font.Bold = true; // Make sum row bold

                worksheet.Cells.AutoFitColumns();

                // Save the file
                File.WriteAllBytes(filePath, package.GetAsByteArray());
                MessageBox.Show($"Report saved to {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Gift Card Formmater
        private void InitializeGiftCardFormmater()
        {
            for (int i = 1; i <= 11; i++)
            {
                Control inputBox = this.Controls.Find($"GiftCardInput{i}", true)[0];
                if (inputBox is TextBox textBox)
                {
                    textBox.TextChanged += GiftCardInput_TextChanged;
                }

            }
        }

        private void GiftCardInput_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox giftCardInput)
            {
                string input = giftCardInput.Text;

                // Match formats like "12345678901234:12:34:123:US$12.34:false" 
                // or "12345678901234 12/34 123 - balance USD$12.34"
                Match match = Regex.Match(input, @"(\d{16})[:\s](\d{2})[:/\s](\d{2})[:\s](\d{3}).*?([\d.]+)");

                if (match.Success)
                {
                    // Format card details
                    string formattedCard = $"Card:{match.Groups[1].Value} | exp:{match.Groups[2].Value}/{match.Groups[3].Value} | cvv:{match.Groups[4].Value}";
                    giftCardInput.Text = formattedCard;

                    // Extract balance from regex match
                    string balance = match.Groups[5].Value;

                    // Determine corresponding BalanceInput field
                    string balanceInputName = giftCardInput.Name.Replace("GiftCardInput", "BalanceInput");
                    Control[] balanceInputs = this.Controls.Find(balanceInputName, true);

                    if (balanceInputs.Length > 0 && balanceInputs[0] is TextBox balanceBox)
                    {
                        balanceBox.Text = match.Groups[5].Value;

                        // Store the original balance only once
                        if (!originalBalances.ContainsKey(balanceInputName))
                        {
                            originalBalances[balanceInputName] = balance;
                        }
                        balanceBox.Text = balance;
                    }
                }
            }
        }
        #endregion

        #region 3min timer buttons
        // Timer 3 Min Buttons
        private void Timer3M_Button1_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button2_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button3_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button4_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button5_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button6_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button7_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button8_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button9_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }
        private void Timer3M_Button10_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer3M_Button11_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }
        #endregion

        #region 5min timer buttons
        // Timer 5 Min Buttons
        private void Timer5M_Button1_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button2_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button3_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button4_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button5_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button6_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button7_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button8_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button9_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button10_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        private void Timer5M_Button11_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }
        #endregion

        #region 15min timer buttons
        // Timer 15 Min Buttons
        private void Timer15M_Button1_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button2_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button3_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button4_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button5_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button6_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button7_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button8_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button9_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button10_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        private void Timer15M_Button11_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }
        #endregion

        #region Labels
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Done Buttons
        private void Done_1_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button1))
            {
                countdowns.Remove(Timer3M_Button1);
                Timer3M_Button1.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button1))
            {
                countdowns.Remove(Timer15M_Button1);
                Timer15M_Button1.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button1))
            {
                countdowns.Remove(Timer5M_Button1);
                Timer5M_Button1.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox1.Image = Resources.yellow;
        }
        private void Done_2_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button2))
            {
                countdowns.Remove(Timer3M_Button2);
                Timer3M_Button2.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button2))
            {
                countdowns.Remove(Timer15M_Button2);
                Timer15M_Button2.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button2))
            {
                countdowns.Remove(Timer5M_Button2);
                Timer5M_Button2.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox2.Image = Resources.yellow;

        }
        private void Done_3_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button3))
            {
                countdowns.Remove(Timer3M_Button3);
                Timer3M_Button3.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button3))
            {
                countdowns.Remove(Timer15M_Button3);
                Timer15M_Button3.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button3))
            {
                countdowns.Remove(Timer5M_Button3);
                Timer5M_Button3.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox3.Image = Resources.yellow;
        }
        private void Done_4_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button4))
            {
                countdowns.Remove(Timer3M_Button4);
                Timer3M_Button4.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button4))
            {
                countdowns.Remove(Timer15M_Button4);
                Timer15M_Button4.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button4))
            {
                countdowns.Remove(Timer5M_Button4);
                Timer5M_Button4.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox4.Image = Resources.yellow;
        }
        private void Done_5_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button5))
            {
                countdowns.Remove(Timer3M_Button5);
                Timer3M_Button5.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button5))
            {
                countdowns.Remove(Timer15M_Button5);
                Timer15M_Button5.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button5))
            {
                countdowns.Remove(Timer5M_Button5);
                Timer5M_Button5.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox5.Image = Resources.yellow;
        }
        private void Done_6_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button6))
            {
                countdowns.Remove(Timer3M_Button6);
                Timer3M_Button6.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button6))
            {
                countdowns.Remove(Timer15M_Button6);
                Timer15M_Button6.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button6))
            {
                countdowns.Remove(Timer5M_Button6);
                Timer5M_Button6.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox6.Image = Resources.yellow;
        }
        private void Done_7_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button7))
            {
                countdowns.Remove(Timer3M_Button7);
                Timer3M_Button7.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button7))
            {
                countdowns.Remove(Timer15M_Button7);
                Timer15M_Button7.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button7))
            {
                countdowns.Remove(Timer5M_Button7);
                Timer5M_Button7.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox7.Image = Resources.yellow;
        }
        private void Done_8_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button8))
            {
                countdowns.Remove(Timer3M_Button8);
                Timer3M_Button8.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button8))
            {
                countdowns.Remove(Timer15M_Button8);
                Timer15M_Button8.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button8))
            {
                countdowns.Remove(Timer5M_Button8);
                Timer5M_Button8.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox8.Image = Resources.yellow;
        }
        private void Done_9_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button9))
            {
                countdowns.Remove(Timer3M_Button9);
                Timer3M_Button9.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button9))
            {
                countdowns.Remove(Timer15M_Button9);
                Timer15M_Button9.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(Timer5M_Button9))
            {
                countdowns.Remove(Timer5M_Button9);
                Timer5M_Button9.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox9.Image = Resources.yellow;
        }
        private void Done_10_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button10))
            {
                countdowns.Remove(Timer3M_Button10);
                Timer3M_Button10.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(button51))
            {
                countdowns.Remove(button51);
                button51.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(button44))
            {
                countdowns.Remove(button44);
                button44.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox10.Image = Resources.yellow;
        }
        private void Done_11_Click(object sender, EventArgs e)
        {
            // stop the timer and reset buttons
            if (countdowns.ContainsKey(Timer3M_Button11))
            {
                countdowns.Remove(Timer3M_Button11);
                Timer3M_Button11.Text = "3 Minutes";
            }
            if (countdowns.ContainsKey(Timer15M_Button11))
            {
                countdowns.Remove(Timer15M_Button11);
                Timer15M_Button11.Text = "15 Minutes";
            }
            if (countdowns.ContainsKey(button53))
            {
                countdowns.Remove(button53);
                button53.Text = "5 Minutes";
            }
            // trigger yellow light image
            lightBox11.Image = Resources.yellow;
        }
        #endregion

        #region Gift Cards inputs
        private void GiftCardInput1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput2_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput3_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput4_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput5_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput6_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput7_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput8_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput9_TextChanged(object sender, EventArgs e)
        {

        }
        private void GiftCardInput10_TextChanged(object sender, EventArgs e)
        {

        }
        private void GiftCardInput11_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Balance Inputs
        private void BalanceInput1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput2_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput3_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput4_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput5_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput6_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput7_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput8_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput9_TextChanged(object sender, EventArgs e)
        {

        }
        private void BalanceInput10_TextChanged(object sender, EventArgs e)
        {

        }
        private void BalanceInput11_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Comment Box inputs
        private void CommentBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommentBox11_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region 1 Dollar Buttons
        private void Button_1Dollar_1_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput1.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                // change lightBox1 to Error image
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_2_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput2.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox2.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_3_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput3.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox3.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_4_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput4.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox4.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_5_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput5.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox5.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_6_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput6.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox6.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_7_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput7.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox7.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_8_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput8.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox8.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_9_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput9.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox9.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Button_1Dollar_10_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput10.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox10.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_11_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput11.Text, out decimal balance))  // Convert text to number
            {
                balance -= 0.99m;  // Subtract 0.99
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox11.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 2 Dollar Buttons
        private void button13_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput1.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput10.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox10.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput9.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox9.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput8.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox8.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput7.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox7.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput6.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox6.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput5.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox5.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput4.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox4.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput3.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox3.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput2.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox2.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput11.Text, out decimal balance))  // Convert text to number
            {
                balance -= 1.99m;  // Subtract 1.99
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox11.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3 Dollar Buttons
        private void button24_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput1.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput10.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox10.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput9.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox9.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput8.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox8.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput7.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox7.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput6.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox6.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput5.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox5.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput4.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox4.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput3.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox3.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput2.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox2.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput11.Text, out decimal balance))  // Convert text to number
            {
                balance -= 2.99m;  // Subtract 2.99
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox11.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 4 Dollar Buttons
        private void button34_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput1.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput10.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox10.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput9.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox9.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput8.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox8.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput7.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox7.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput6.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox6.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput5.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox5.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput4.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox4.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput3.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox3.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput2.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox2.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput11.Text, out decimal balance))  // Convert text to number
            {
                balance -= 3.99m;  // Subtract 3.99
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox11.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 5 Dollar Buttons
        private void Button_5Dollar_1_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput1.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_5Dollar_2_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput2.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox2.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_5Dollar_3_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput3.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox3.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput3.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox3.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_4_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput4.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox4.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_5_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput5.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox5.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_6_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput6.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox6.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_7_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput7.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox7.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_8_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput8.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox8.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_9_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput9.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox9.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button48_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput10.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox10.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(BalanceInput11.Text, out decimal balance))  // Convert text to number
            {
                balance -= 4.99m;  // Subtract 4.99
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox11.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Clear All function
        private void ButtonClearAll_Click(object sender, EventArgs e)
        {
            // Clear all inputs
            GiftCardInput1.Text = "";
            GiftCardInput2.Text = "";
            GiftCardInput3.Text = "";
            GiftCardInput4.Text = "";
            GiftCardInput5.Text = "";
            GiftCardInput6.Text = "";
            GiftCardInput7.Text = "";
            GiftCardInput8.Text = "";
            GiftCardInput9.Text = "";
            GiftCardInput10.Text = "";
            GiftCardInput11.Text = "";

            BalanceInput10.Text = "";
            BalanceInput1.Text = "";
            BalanceInput2.Text = "";
            BalanceInput3.Text = "";
            BalanceInput4.Text = "";
            BalanceInput5.Text = "";
            BalanceInput6.Text = "";
            BalanceInput7.Text = "";
            BalanceInput8.Text = "";
            BalanceInput9.Text = "";
            BalanceInput11.Text = "";

            CommentBox1.Text = "";
            CommentBox2.Text = "";
            CommentBox3.Text = "";
            CommentBox4.Text = "";
            CommentBox5.Text = "";
            CommentBox6.Text = "";
            CommentBox7.Text = "";
            CommentBox8.Text = "";
            CommentBox9.Text = "";
            CommentBox10.Text = "";
            CommentBox11.Text = "";
        }
        #endregion

        #region Save GiftCardsData function
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Gift Card Data";
                saveFileDialog.FileName = "GiftCard_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, GetGiftCardData());
                    MessageBox.Show("Gift Card Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        private string GetGiftCardData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"GiftCard: {GiftCardInput1.Text}, Balance: {BalanceInput1.Text}, Comment: {CommentBox1.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput2.Text}, Balance: {BalanceInput2.Text}, Comment: {CommentBox2.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput3.Text}, Balance: {BalanceInput3.Text}, Comment: {CommentBox3.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput4.Text}, Balance: {BalanceInput4.Text}, Comment: {CommentBox4.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput5.Text}, Balance: {BalanceInput5.Text}, Comment: {CommentBox5.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput6.Text}, Balance: {BalanceInput6.Text}, Comment: {CommentBox6.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput7.Text}, Balance: {BalanceInput7.Text}, Comment: {CommentBox7.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput8.Text}, Balance: {BalanceInput8.Text}, Comment: {CommentBox8.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput9.Text}, Balance: {BalanceInput9.Text}, Comment: {CommentBox9.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput10.Text}, Balance: {BalanceInput10.Text}, Comment: {CommentBox10.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput11.Text}, Balance: {BalanceInput11.Text}, Comment: {CommentBox11.Text}");
            return sb.ToString();
        }
        #endregion

        #region lightboxes
        private void lightBox8_Click(object sender, EventArgs e)
        {

        }

        private void lightBox7_Click(object sender, EventArgs e)
        {

        }

        private void lightBox6_Click(object sender, EventArgs e)
        {

        }

        private void lightBox5_Click(object sender, EventArgs e)
        {

        }

        private void lightBox4_Click(object sender, EventArgs e)
        {

        }

        private void lightBox3_Click(object sender, EventArgs e)
        {

        }

        private void lightBox2_Click(object sender, EventArgs e)
        {

        }

        private void lightBox9_Click(object sender, EventArgs e)
        {

        }
        private void lightBox1_Click(object sender, EventArgs e)
        {
            // if 
        }
        private void lightBox11_Click(object sender, EventArgs e)
        {

        }
        private void lightBox10_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region App Grid
        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
        #endregion

        #region Unimportant
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void GiftCardManager_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Exit Button
        private void Exit_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }
        #endregion
    }
}
