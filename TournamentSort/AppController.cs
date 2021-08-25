using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentSort
{
    class AppController
    {
        public static void ShowArrayInput(InputView form, Models.ArrayModel model)
        {
            Label label1 = new Label();
            label1.Text = "Введите числа для сортировки через пробел \nНапример: 1 2 3";
            label1.Location = new System.Drawing.Point(13, 13);
            label1.Size = new System.Drawing.Size(600, 50);
            label1.ForeColor = System.Drawing.Color.FromName("white");
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            label1.AutoSize = true;


            form.textBox2.Location = new System.Drawing.Point(14, 93);
            form.textBox2.Size = new System.Drawing.Size(500, 80);
            form.textBox2.ForeColor = System.Drawing.Color.FromName("white");
            form.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            form.textBox2.BackColor = System.Drawing.Color.FromArgb(148, 115, 49);

            Button button1 = new Button();
            button1.Text = "Принять";
            button1.Location = new System.Drawing.Point(13, 143);
            button1.Size = new System.Drawing.Size(100, 50);
            button1.ForeColor = System.Drawing.Color.FromName("white");
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            button1.BackColor = System.Drawing.Color.FromArgb(148, 115, 49);
            button1.Click += (sender, e) => { ParseArrayInput(form, model); };

            form.Controls.Add(button1);
            form.Controls.Add(label1);
            form.Controls.Add(form.textBox2);
            form.textBox2.Show();
        }

        public static void ParseArrayInput(InputView form, Models.ArrayModel model)
        {
            model.unsortedArray = new int[model.elementsCount];
            model.array = new int[model.elementsCount];

            string elementsRow =  form.textBox2.Text;
            string[] digits = elementsRow.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < model.elementsCount; i++)
            {
                model.unsortedArray[i] = Convert.ToInt32(digits[i]);
                model.array[i] = Convert.ToInt32(digits[i]);
            }

            AlgView nextForm = new AlgView(model);

            Label algLabel = new Label();

            nextForm.Controls.Add(algLabel);
            nextForm.Show();
            form.Hide();
            ShowAlg(nextForm, model);
        }

        public static void ShowAlg(AlgView form, Models.ArrayModel model)
        {
            form.Controls.Add(new Label()
            {
                Name = "title",
                Text = "Несортированный массив с данными: ",
                Location = new System.Drawing.Point(13, 23),
                AutoSize = true,
                ForeColor = System.Drawing.Color.FromName("White")
            });

            ShowArrayRow(form, model);

            Button submitButton = new Button();
            submitButton.Text = "Построить дерево!";
            submitButton.AutoSize = true;
            submitButton.Location = new System.Drawing.Point(13, 93);
            submitButton.Click += (sender, e) => { Algorithm(form, model); submitButton.Hide(); form.Controls["title"].Text = "Турнирное дерево:"; };

            form.Controls.Add(submitButton);
        }

        public static void Algorithm(AlgView form, Models.ArrayModel model)
        {
            BuildTournamentHeap(form, model);
        }

        public static void ShowArrayRow(AlgView form, Models.ArrayModel model)
        {
            int i = 0;
            foreach (var item in model.unsortedArray)
            {
                form.Controls.Add(new Label()
                {
                    Name = "label" + i.ToString(),
                    Text = $"{item}",
                    Location = new System.Drawing.Point((600 / model.elementsCount / 2) + (600 / model.elementsCount) * i, 53),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.FromName("White"),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14)
                }) ; 
                i++;
            }
        }

        public static async void BuildTournamentHeap(AlgView form, Models.ArrayModel model)
        {
            if (model.elementsCount == 1)
            { 
                ShowArrays(form, model);
                Button resButton = new Button();
                resButton.Text = "Показать результаты!";
                resButton.AutoSize = true;
                resButton.Click += (sender, e) =>
                {
                    form.Controls.Clear();
                    model.sortedArray.Add(model.unsortedArray[0]);
                    ResultView nextForm = new ResultView(model);
                    form.Hide();
                    nextForm.Show();

                    Label label1 = new Label();
                    label1.Text = "Начальный массив для сортировки:";
                    label1.Location = new System.Drawing.Point(13, 13);
                    label1.AutoSize = true;
                    label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
                    label1.ForeColor = System.Drawing.Color.FromName("White");
                    foreach (var item in model.array)
                    {
                        label1.Text += $" {item}";
                    }

                    Label label2 = new Label();
                    label2.Text = "Отсортированный массив:";
                    label2.Location = new System.Drawing.Point(13, 63);
                    label2.AutoSize = true;
                    label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
                    label2.ForeColor = System.Drawing.Color.FromName("White");
                    foreach (var item in model.sortedArray)
                    {
                        label2.Text += $" {item}";
                    }

                    Label label3 = new Label();
                    label3.Text = "Сложность алгоритма = O(n*log(n))";
                    label3.Location = new System.Drawing.Point(13, 113);
                    label3.AutoSize = true;
                    label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
                    label3.ForeColor = System.Drawing.Color.FromName("White");

                    nextForm.BackColor = System.Drawing.Color.FromArgb(49, 82, 148);
                    nextForm.AutoSize = true;

                    nextForm.Controls.Add(label1);
                    nextForm.Controls.Add(label2);
                    nextForm.Controls.Add(label3);
                };
                resButton.Location = new System.Drawing.Point(300, 315);
                form.Controls.Add(resButton);
                return;
            }
            int[] currentLevel = model.unsortedArray;
            int[] nextLevel;
            int multiplyer;
            int j = 0;
            while (currentLevel.Length != 1)
            {
                if (currentLevel.Length % 2 == 0)
                {
                    nextLevel = new int[currentLevel.Length / 2];
                }
                else
                {
                    nextLevel = new int[currentLevel.Length / 2 + 1];
                }

                for (int i = 0; i < currentLevel.Length; i += 2)
                {
                    if ((i == currentLevel.Length - 1) || (currentLevel[i] <= currentLevel[i + 1]))
                    {
                        nextLevel[i / 2] = currentLevel[i];
                    }
                    else
                    {
                        nextLevel[i / 2] = currentLevel[i + 1];
                    }

                    if (i == 0) { multiplyer = 1; } else { multiplyer = i; };
                    form.Controls.Add(new Label()
                    {
                        Name = "element" + i.ToString() + j.ToString(),
                        Text = $"{nextLevel[i / 2]}",
                        Location = new System.Drawing.Point((600 / nextLevel.Length / 2) + (600 / nextLevel.Length) * (i / 2), 100 + 35 * j),
                        AutoSize = true,
                        ForeColor = System.Drawing.Color.FromName("White"),
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 14)
                    });
                    await Task.Delay(1000);
                }
                j++;

                currentLevel = nextLevel;

                Button restartButton = new Button();
                restartButton.Location = new System.Drawing.Point(400, 315);
                restartButton.Text = "Повторить шаг сложностью O(logN)!";
                restartButton.AutoSize = true;
                restartButton.Click += (sender, e) => { form.Controls.Clear(); ShowArrayRow(form, model); Algorithm(form, model); restartButton.Hide(); };

                form.Controls.Add(restartButton);
            }

                form.Controls["element0" + (j - 1).ToString()].BackColor = System.Drawing.Color.FromName("Green");

            int min = int.Parse(form.Controls["element0" + (j-1).ToString()].Text);


            model.unsortedArray = model.unsortedArray.Where(val => val != min).ToArray();
            model.sortedArray.Add(min);
            model.elementsCount--;
            
            ShowArrays(form, model);
        }

        public static void ShowArrays(Form form, Models.ArrayModel model)
        {
            Label unsortedArrayLabel = new Label();
            unsortedArrayLabel.Text = "Неотсортированные элементы:";
            foreach (var item in model.unsortedArray)
            {
                unsortedArrayLabel.Text += $" {item}";
            }
            unsortedArrayLabel.Location = new System.Drawing.Point(13, 300);
            unsortedArrayLabel.AutoSize = true;
            unsortedArrayLabel.ForeColor = System.Drawing.Color.FromName("White");
            unsortedArrayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);

            Label sortedArrayLabel = new Label();
            sortedArrayLabel.Text = "Отсортиованные элементы:";
            foreach (var item in model.sortedArray)
            {
                sortedArrayLabel.Text += $" {item}";
            }
            sortedArrayLabel.Location = new System.Drawing.Point(13, 330);
            sortedArrayLabel.AutoSize = true;
            sortedArrayLabel.ForeColor = System.Drawing.Color.FromName("White");
            sortedArrayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);

            form.Controls.Add(unsortedArrayLabel);
            form.Controls.Add(sortedArrayLabel);
        }
    }
}
