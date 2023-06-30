namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        cAI AI { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AI = new cAI(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AI.Train();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AI.Test();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            AI.SingleTest();
        }
    }
}