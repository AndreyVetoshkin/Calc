using CalcLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCalculator
{
    public partial class WinFormsCalculator : Form
    {
        private Calculator Calc { get; set; }
        private IList<string> Favorites { get; set; }

        private IOperation CurrentSelectedOperation { get; set; }
        private void Calculate()
        {
            timer1.Stop();
            var inputData = StringConverter(tbInput.Text);

            //вычислить
            var result = CurrentSelectedOperation.Execute(inputData);
            lblResult.Text = $"{result}";
        }

        public WinFormsCalculator()
        {            
            Favorites = new List<string>() { "sum" };
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WinFormsCalculator_Load(object sender, EventArgs e)
        {
            Calc = new Calculator();

            var operations = CalcHelper.GetOperations();

            foreach (var oper in operations)
            {
                Calc.Operations.Add(oper);
            }

            cbOperation.DataSource = Calc.Operations;
            cbOperation.DisplayMember = "Name";
            cbOperation.ValueMember = "Name";
            cbOperation.SelectedIndexChanged += new System.EventHandler(this.cbOperation_SelectedIndexChanged);

            CurrentSelectedOperation = cbOperation.SelectedItem as IOperation;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            //понять какая операция выбрана
            if (CurrentSelectedOperation == null)
            {
                lblResult.Text = "Выберите операцию";
                return;
            }
            //получить входные данные

            //вычислить
            Calculate();
        }

        private void cbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //понять какая операция выбрана
            CurrentSelectedOperation = cbOperation.SelectedItem as IOperation;

            //проверить избранная кнопка или нет
            //показать кнопку
            btnLike.Visible = !Favorites.Contains(CurrentSelectedOperation.Name);
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            //добавить кнопку
            #region Добавляем кнопку
            var button = new System.Windows.Forms.Button();
            button.Location = new System.Drawing.Point(3, 3);
            button.Name = "btn"+CurrentSelectedOperation.Name;
            button.Size = new System.Drawing.Size(75, 23);
            button.TabIndex = 7;
            button.Text = CurrentSelectedOperation.Name;
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(btn_Click);

            flowLayoutPanel1.Controls.Add(button);
            #endregion

            // добавляем операцию в список избранных
            Favorites.Add(CurrentSelectedOperation.Name);

            // скрыть кнопку
            btnLike.Visible = false;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            cbOperation.SelectedItem = Calc.Operations.FirstOrDefault(o => o.Name == button.Text);

            Calculate();
        }

        static double[] StringConverter(string args)
        {
            var result = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(s => s.ToDouble())
                             .ToArray();
            return result;
        }

        private void tbInput_Click(object sender, EventArgs e)
        {
            tbInput.SelectAll();
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            if (e.KeyCode == Keys.Enter)
                Calculate();          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
