using CalcLibrary;
using DBModel;
using DBModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private IList<string> favorites { get; set; }

        private IEnumerable<IOperation> _3rdParties;

        private IOperation CurrentSelectedOperation { get; set; }
        private void Calculate()
        {
            timer1.Stop();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var inputData = StringConverter(tbInput.Text);

            //ДЗ от Игоря
            //проверить среднее время выполнения операции
            //если меньше 1 секунды - выполняем сразу
            //иначе ищем в базе операцию с такими же параметрами
            //если нашли - возвращаем результат
            //иначе - считаем

            //вычислить
            var heavyOp = DB.GetHeavyOperations().FirstOrDefault(o => o == CurrentSelectedOperation.Name);
            double result;
            if (heavyOp == null)
            {
                result = CurrentSelectedOperation.Execute(inputData);
            }
            else
            {
                var DBOperation = DB.GetOperationHistory(CurrentSelectedOperation.Name, tbInput.Text);
                if (DBOperation != null && DBOperation.Result != null)
                {
                    result = (double)DBOperation.Result;
                }
                else
                {
                    result = CurrentSelectedOperation.Execute(inputData);
                }
            }


            lblResult.Text = $"{result}";
            stopWatch.Stop();

            OperationHistory opHistory = new OperationHistory();
            opHistory.Name = CurrentSelectedOperation.Name;
            opHistory.Args = tbInput.Text;
            opHistory.Result = result;
            opHistory.ExecTime = stopWatch.ElapsedMilliseconds;
            DB.AddOperationHistory(opHistory);
        }

        public WinFormsCalculator()
        {
            InitializeComponent();
            favorites = new List<string>();
            foreach (var fav in DB.GetFavorites())
            {
                AddFavoriteButton(fav.Name);
                favorites.Add(fav.Name);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WinFormsCalculator_Load(object sender, EventArgs e)
        {
            Calc = new Calculator();

            var operations = CalcHelper.GetOperations();
            _3rdParties = operations;

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
            btnLike.Visible = !favorites.Contains(CurrentSelectedOperation.Name);
            btnDislike.Visible = favorites.Contains(CurrentSelectedOperation.Name);
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            //добавить кнопку
            AddFavoriteButton(CurrentSelectedOperation.Name);
            // добавляем операцию в список избранных
            favorites.Add(CurrentSelectedOperation.Name);
            if (_3rdParties.Contains(CurrentSelectedOperation))
                DB.AddFavorite(new Favorite(CurrentSelectedOperation.Name, true));
            else
                DB.AddFavorite(new Favorite(CurrentSelectedOperation.Name));
            // скрыть кнопку
            btnLike.Visible = false;
            btnDislike.Visible = true;          
        }

        private void AddFavoriteButton(string name)
        {
            var button = new System.Windows.Forms.Button();
            button.Location = new System.Drawing.Point(3, 3);
            button.Name = "btn" + name;
            button.Size = new System.Drawing.Size(75, 23);
            button.TabIndex = 7;
            button.Text = name;
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(btn_Click);

            flowLayoutPanel1.Controls.Add(button);
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

        private void btnDislike_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.RemoveByKey("btn" + CurrentSelectedOperation.Name);
            favorites.Remove(CurrentSelectedOperation.Name);
            DB.DeleteFavorite(CurrentSelectedOperation.Name);
            btnDislike.Visible = false;
            btnLike.Visible = true;
        }
    }
}
