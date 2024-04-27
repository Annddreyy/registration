using QRCoder;
using System.Data;
using System.Data.SqlClient;

namespace Регистрация_Пациентов
{
    public partial class QRCodeGeneratorWindow : Form
    {
        int ID;

        public QRCodeGeneratorWindow(int id)
        {
            ID = id;
            this.Load += new EventHandler(QRCode_Load);
            InitializeComponent();
        }

        private void QRCode_Load(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ID.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            QRCodeImage.Image = qrCode.GetGraphic(5);
        }

        private void QRButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-7FQ2GETM\SQLEXPRESS;Initial Catalog=МУЗ Городская клиническая больница;Integrated Security=True";
            string sql = "SELECT * FROM Пациенты";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    int Id = (int)cells[0];
                    if (Id == ID)
                    {
                        string message = "";
                        for (int i = 2; i < cells.Length; i++)
                        {
                            message += dt.Columns[i].ColumnName + ": " + cells[i].ToString() + "\n";
                        }
                        MessageBox.Show(message);
                        break;
                    }
                }
            }
        }
    }
}
