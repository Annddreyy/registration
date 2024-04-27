using System.Data;
using System.Data.SqlClient;

namespace Регистрация_Пациентов
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();

            LastNameTextBox.KeyDown += new KeyEventHandler(nextTextBox);
            NameTextBox.KeyDown += new KeyEventHandler(nextTextBox);
            PatronymicTextBox.KeyDown += new KeyEventHandler(nextTextBox);
            PassportTextBox.KeyDown += new KeyEventHandler(nextTextBox);
            AdressTextBox.KeyDown += new KeyEventHandler(nextTextBox);
            PhoneTextBox.KeyDown += new KeyEventHandler(nextTextBox);
            EmailTextBox.KeyDown += new KeyEventHandler(nextTextBox);
        }

        void nextTextBox(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender == LastNameTextBox)
                    NameTextBox.Focus();
                else if (sender == NameTextBox)
                    PatronymicTextBox.Focus();
                else if (sender == PatronymicTextBox)
                    PassportTextBox.Focus();
                else if (sender == PassportTextBox)
                    AdressTextBox.Focus();
                else if (sender == AdressTextBox)
                    PhoneTextBox.Focus();
                else if (sender == PhoneTextBox)
                    EmailTextBox.Focus();
                else
                    PolisTextBox.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            LastNameTextBox.Text = "";
            NameTextBox.Text = "";
            PatronymicTextBox.Text = "";
            PassportTextBox.Text = "";
            AdressTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
            BirthdayTimePicker.Value = DateTime.Now;
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-7FQ2GETM\SQLEXPRESS;Initial Catalog=МУЗ Городская клиническая больница;Integrated Security=True";
            string sql = "SELECT * FROM Пациенты";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();

                int id = (int)dt.Rows[^1].ItemArray[0];
                newRow["ID"] = id + 1;
                newRow["Имя"] = NameTextBox.Text;
                newRow["Фамилия"] = LastNameTextBox.Text;
                newRow["Пол"] = (PolRadioButton1.Checked) ? "М" : "Ж";
                newRow["Отчество"] = PatronymicTextBox.Text;
                newRow["Паспорт"] = PassportTextBox.Text;
                newRow["Адрес"] = AdressTextBox.Text;
                newRow["Телефон"] = PhoneTextBox.Text;
                newRow["Email"] = EmailTextBox.Text;
                newRow["ДатаРождения"] = BirthdayTimePicker.Value;
                newRow["ИсторияБолезниПациента"] = "";

                byte[] img;
                if (PolRadioButton1.Checked)
                    img = File.ReadAllBytes("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\M.jpg");
                else
                    img = File.ReadAllBytes("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\F.jpg");
                newRow["Фото"] = img;
                newRow["ДатаПоследнегоОбрашения"] = "2022-11-22";
                newRow["ДатаСледующегоВизита"] = "2022-11-22";
                dt.Rows.Add(newRow);

                Word helper = new Word("Согласие на обработку персональных данных.docx");
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"<LastName>", newRow["Фамилия"].ToString()},
                    {"<Name>", newRow["Имя"].ToString()},
                    {"<Patronymic>", newRow["Отчество"].ToString()},
                    {"<Passport>", newRow["Паспорт"].ToString()},
                    {"<Adress>", newRow["Адрес"].ToString()},
                };

                helper.Process(dic);

                helper = new Word("Договор.docx");

                string FIO = newRow["Фамилия"].ToString() + " " + newRow["Имя"].ToString() + " " + newRow["Отчество"].ToString();
                dic = new Dictionary<string, string>()
                {
                    {"<FIO>",  FIO},
                    {"<Passport>", newRow["Паспорт"].ToString()},
                    {"<Adress>", newRow["Адрес"].ToString()},
                    {"<Phone>", newRow["Телефон"].ToString()},
                };
                helper.Process(dic);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                ds.Clear();
                adapter.Fill(ds);

                QRCodeGeneratorWindow form = new QRCodeGeneratorWindow(id + 1);
                form.Show();

            }
        }
    }
}
