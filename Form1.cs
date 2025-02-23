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



namespace Card_Manager
{
    public partial class GiftCardManager : Form
    {
        private Timer countdownTimer;
        private Dictionary<Button, int> countdowns = new Dictionary<Button, int>();
        private readonly ToolTip toolTip = new ToolTip();

        public GiftCardManager()
        {
            InitializeComponent();
            InitializeCountdownTimer();
        }

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

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Copy_1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput1.Text))
            {
                Clipboard.SetText(GiftCardInput1.Text);
                toolTip.Show("Copied!", copy_1, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Copy_2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput2.Text))
            {
                Clipboard.SetText(GiftCardInput2.Text);
                toolTip.Show("Copied!", copy_2, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Copy_3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput3.Text))
            {
                Clipboard.SetText(GiftCardInput3.Text);
                toolTip.Show("Copied!", copy_3, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copy_4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput4.Text))
            {
                Clipboard.SetText(GiftCardInput4.Text);
                toolTip.Show("Copied!", copy_4, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copy_5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput5.Text))
            {
                Clipboard.SetText(GiftCardInput5.Text);
                toolTip.Show("Copied!", copy_5, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copy_6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput6.Text))
            {
                Clipboard.SetText(GiftCardInput6.Text);
                toolTip.Show("Copied!", copy_6, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copy_7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput7.Text))
            {
                Clipboard.SetText(GiftCardInput7.Text);
                toolTip.Show("Copied!", copy_7, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copy_8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput8.Text))
            {
                Clipboard.SetText(GiftCardInput8.Text);
                toolTip.Show("Copied!", copy_8, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copy_9_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GiftCardInput9.Text))
            {
                Clipboard.SetText(GiftCardInput9.Text);
                toolTip.Show("Copied!", copy_9, 1000);
            }
            else
            {
                MessageBox.Show("GiftCardInput is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// GiftCardInput_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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

        /// <summary>
        /// BalanceInput_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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

        /// <summary>
        /// Button_1Dollar_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_1Dollar_1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput1.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                // change pictureBox1 to Error image
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput2.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput3.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput4.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_5_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput5.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput6.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput7.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_8_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput8.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_1Dollar_9_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput9.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Button_5Dollar_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_5Dollar_1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput1.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput1.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_5Dollar_2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput2.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_5Dollar_3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput3.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput4.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_5_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput5.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput6.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput7.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_8_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput8.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_5Dollar_9_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput9.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 5
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                pictureBox1.Image = Properties.Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClearAll_Click(object sender, EventArgs e)
        {
            // Clear all the GiftCardInput and BalanceInput
            GiftCardInput1.Text = "";
            GiftCardInput2.Text = "";
            GiftCardInput3.Text = "";
            GiftCardInput4.Text = "";
            GiftCardInput5.Text = "";
            GiftCardInput6.Text = "";
            GiftCardInput7.Text = "";
            GiftCardInput8.Text = "";
            GiftCardInput9.Text = "";
            BalanceInput1.Text = "";
            BalanceInput2.Text = "";
            BalanceInput3.Text = "";
            BalanceInput4.Text = "";
            BalanceInput5.Text = "";
            BalanceInput6.Text = "";
            BalanceInput7.Text = "";
            BalanceInput8.Text = "";
            BalanceInput9.Text = "";
        }
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
            sb.AppendLine($"GiftCard: {GiftCardInput1.Text}, Balance: {BalanceInput1.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput2.Text}, Balance: {BalanceInput2.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput3.Text}, Balance: {BalanceInput3.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput4.Text}, Balance: {BalanceInput4.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput5.Text}, Balance: {BalanceInput5.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput6.Text}, Balance: {BalanceInput6.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput7.Text}, Balance: {BalanceInput7.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput8.Text}, Balance: {BalanceInput8.Text}");
            sb.AppendLine($"GiftCard: {GiftCardInput9.Text}, Balance: {BalanceInput9.Text}");
            return sb.ToString();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            // if 
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
            {
                List<Button> buttons = new List<Button>(countdowns.Keys);
                List<Button> finishedButtons = new List<Button>();

                foreach (Button button in buttons)
                {
                    int remainingTime = countdowns[button] - 1;

                    if (remainingTime <= 0)
                    {
                        button.Text = "Time's Up!";
                        finishedButtons.Add(button);

                        // 🔊 Play a notification sound
                        string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "alarm.wav");
                    SoundPlayer player = new SoundPlayer(soundPath);
                    player.Play();

                    // ⏪ Reset button text to its original name (optional)
                    button.Text = button.Name;

                    // change pictureBox1 to normal image
                    pictureBox1.Image = Properties.Resources.green;
                }
                else
                    {
                        button.Text = FormatTime(remainingTime);
                        countdowns[button] = remainingTime;
                    }
                }

                // Remove finished buttons from dictionary
                foreach (Button button in finishedButtons)
                {
                    countdowns.Remove(button);
                }

                // Stop timer if no active countdowns
                if (countdowns.Count == 0)
                {
                    countdownTimer.Stop();
                }
            }

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

        private void Timer15M_Button1_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button8_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button7_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button6_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button5_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button4_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button3_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button2_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer15M_Button9_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void GiftCardManager_Load(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }
    }
}
