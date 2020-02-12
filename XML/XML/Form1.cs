using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XML
{
    public partial class Form1 : Form
    {
        XDocument doc;
        const string STUDENT = "student";
        const string FNAME = "firstName";
        const string LNAME = "lastName";
        const string ADDRESS = "Address";
        const string STREET = "street";
        const string HOUSE = "house";
        const string GRADE = "grade";
        const string SUBJECT = "Subject";
        const string MARK = "";




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFromXml();

        }

        private void ReadFromXml()
        {
            try
            {
                doc = XDocument.Load(ConfigurationManager.AppSettings["xmlFilePath"]);
                var st = doc.Descendants(STUDENT).First(a => a.Attribute("id").Value == "3433");
                textBox1.Text = st.Element("firstName").Value;
                textBox2.Text = st.Element("lastName").Value;
                dataGridView1.DataSource = st.Elements("Grades").FirstOrDefault().Elements("grade").Select(el=> new {subject= el.Attribute("Subject").Value , mark=el.Attribute("mark").Value  }).ToList();
                toolStripStatusLabel1.Text = "שליפה תקינה !!";
                 
            }
            catch (Exception)
            {
                toolStripStatusLabel1.Text = "שגיאה !!";
            
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument doc = new XDocument();
                var root = new XElement ("MyClassRoot");
                doc.Add(root);
                for (int i = 0; i < 10; i++)
                {
                    root.Add(new XElement("Student",new XElement("FName", "תלמיד" + i), new XElement("LName", "aaa" + i),new XAttribute ("id",i)));
                }
                doc.Save(ConfigurationManager.AppSettings["ExportFilePath"]);
                toolStripStatusLabel1.Text = "  נשמר !!";

            }
            catch (Exception)
            {

                toolStripStatusLabel1.Text = "  בשמירה שגיאה !!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myStElement =
            doc.Descendants("student").First(s => s.Attribute("id").Value == "3433");
            myStElement.Element("firstName").Value = textBox1.Text;
            myStElement.Element("lastName").Value = textBox2.Text;

            myStElement.Element("Address").Element  ("street").Value = textBox3.Text   ;
            doc.Save(ConfigurationManager.AppSettings["xmlFilePath"]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doc.Root.Add(new XElement(STUDENT, new XElement(FNAME, textBox1.Text), new XElement(LNAME, textBox2.Text)));
            doc.Save(ConfigurationManager.AppSettings["xmlFilePath"]);
        }
    }
}
