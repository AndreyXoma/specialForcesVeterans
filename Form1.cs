namespace specialForcesVeterans
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateZakaz fr = new CreateZakaz();
            fr.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Services fr = new Services();
            fr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActiveZakaz fr = new ActiveZakaz();
            fr.Show();
            this.Hide();
        }
    }
}