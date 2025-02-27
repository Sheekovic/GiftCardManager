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

        private void Done_1_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox1.Image = Resources.red;
        }


        private void Done_2_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox2.Image = Resources.red;

        }

        private void Done_3_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox3.Image = Resources.red;
        }

        private void Done_4_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox4.Image = Resources.red;
        }

        private void Done_5_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox5.Image = Resources.red;
        }

        private void Done_6_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox6.Image = Resources.red;
        }

        private void Done_7_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox7.Image = Resources.red;
        }

        private void Done_8_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox8.Image = Resources.red;
        }

        private void Done_9_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox9.Image = Resources.red;
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
            if (int.TryParse(BalanceInput2.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
                lightBox1.Image = Resources.red;
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
            GiftCardInput10.Text = "";
            BalanceInput10.Text = "";
            BalanceInput11.Text = "";
            GiftCardInput11.Text = "";
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
            sb.AppendLine($"GiftCard: {GiftCardInput10.Text}, Balance: {GiftCardInput11.Text}");
            sb.AppendLine($"GiftCard: {BalanceInput10.Text}, Balance: {BalanceInput11.Text}");
            return sb.ToString();
        }

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

        private async void CountdownTimer_Tick(object sender, EventArgs e)
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

                    // wait for 5 seconds
                    await Task.Delay(5000);

                    // ⏪ Reset button text to its original name (optional)
                    button.Text = button.Name;

                    // change lightBox1 to normal image
                    lightBox1.Image = Properties.Resources.green;
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

        private void Timer5M_Button1_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button10_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button9_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button8_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button7_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button6_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button5_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button4_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button3_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button2_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void Timer5M_Button11_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 300); // 5 minutes
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput1.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
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
            if (int.TryParse(BalanceInput10.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput9.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput8.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput7.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput6.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput5.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput4.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput3.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput2.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput11.Text, out int balance))  // Convert text to number
            {
                balance -= 2;  // Subtract 1
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput1.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
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
            if (int.TryParse(BalanceInput10.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput9.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput8.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput7.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput6.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput5.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput4.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput3.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput2.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput11.Text, out int balance))  // Convert text to number
            {
                balance -= 3;  // Subtract 1
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput1.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
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
            if (int.TryParse(BalanceInput10.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput9.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput9.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput8.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput8.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput7.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput7.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput6.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput6.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput5.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput5.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput4.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput4.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput3.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput3.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput2.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput2.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput11.Text, out int balance))  // Convert text to number
            {
                balance -= 4;  // Subtract 1
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Done_10_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
            if (countdowns.ContainsKey(button52))
            {
                countdowns.Remove(button52);
                button52.Text = "3 Minutes";
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
            // trigger red light image
            lightBox10.Image = Resources.red;
        }

        private void Done_11_Click(object sender, EventArgs e)
        {
            // stop the timer in the first row
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
            // trigger red light image
            lightBox11.Image = Resources.red;
        }

        private void GiftCardInput10_TextChanged(object sender, EventArgs e)
        {

        }

        private void GiftCardInput11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput10.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput11.Text, out int balance))  // Convert text to number
            {
                balance -= 1;  // Subtract 1
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput10.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 1
                BalanceInput10.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (int.TryParse(BalanceInput11.Text, out int balance))  // Convert text to number
            {
                balance -= 5;  // Subtract 1
                BalanceInput11.Text = balance.ToString();  // Update the TextBox
                lightBox1.Image = Resources.red;
            }
            else
            {
                MessageBox.Show("Invalid balance value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BalanceInput10_TextChanged(object sender, EventArgs e)
        {

        }

        private void BalanceInput11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Timer3M_Button10_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer15M_Button10_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void Timer3M_Button11_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 180); // 3 minutes
        }

        private void Timer15M_Button11_Click(object sender, EventArgs e)
        {
            StartCountdown((Button)sender, 900); // 15 minutes
        }

        private void lightBox11_Click(object sender, EventArgs e)
        {

        }

        private void lightBox10_Click(object sender, EventArgs e)
        {

        }

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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
