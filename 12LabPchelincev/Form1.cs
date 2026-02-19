using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12LabPchelincev
{
    public partial class Form1: Form
    {
        private int[] array;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.RowCount = 7;
            string[] algArr = {
                "Обмен",
                "Выбор",
                "Включение",
                "Шелла",
                "Быстрая",
                "Линейная",
                "Встроенная"
            };

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = algArr[i];
            }
        }

        public void generate_Array(int arraySize)
        {
            array = new int[arraySize];
            Random rnd = new Random();

            for (int i = 0; i < arraySize; i++)
            {
                array[i] = rnd.Next(0, arraySize);
            }
        }

        public void bubble_Sort(int[] cloneArray, out int comps, out int searchTime, out int swapCount)
        {
            int len = cloneArray.Length;
            comps = 0;
            swapCount = 0;
            bool flag = true;

            int startTime = Environment.TickCount;
            for (int i = 0; i < len && flag; i++)
            {
                flag = false;
                for (int j = 0; j < len - 1 - i; j++)
                {
                    comps++;
                    if (cloneArray[j] > cloneArray[j + 1])
                    {
                        (cloneArray[j], cloneArray[j + 1]) = (cloneArray[j + 1], cloneArray[j]);
                        swapCount++;
                        flag = true;
                    }
                }
            }
            searchTime = Environment.TickCount - startTime;
        }

        public void choice_Sort(int[] cloneArray, out int comps, out int searchTime, out int swapCount)
        {
            int len = cloneArray.Length;
            comps = 0;
            swapCount = 0;

            int startTime = Environment.TickCount;
            for (int i = 0; i < len-1; i++)
            {
                int index = i;
                for (int j = i + 1; j < len; j++)
                {
                    comps++;
                    if (cloneArray[j] < cloneArray[index])
                    {
                        index = j;
                    }
                }

                ( cloneArray[i], cloneArray[index]) = (cloneArray[index], cloneArray[i]);
                swapCount++;
            }
            searchTime = Environment.TickCount - startTime;
        }

        public void inclusion_Sort(int[] cloneArray, out int comps, out int searchTime, out int swapCount)
        {
            int len = cloneArray.Length;
            comps = 0;
            swapCount = 0;
            int minIndex = 0;
            for (int i = 0; i < len; i++)
            {
                if (cloneArray[i] < cloneArray[minIndex])
                {
                    minIndex = i;
                }
            }

            (cloneArray[0], cloneArray[minIndex]) = (cloneArray[minIndex], cloneArray[0]);
            swapCount++;
            int startTime = Environment.TickCount;
            for (int i = 2; i < len; i++)
            {
                int currentElem = cloneArray[i];
                int j = i - 1;
                while (cloneArray[j] > currentElem)
                {
                    cloneArray[j + 1] = cloneArray[j];
                    swapCount++;
                    comps++;
                    j--;
                }
                comps++;
                cloneArray[j + 1] = currentElem;
                swapCount++;
            }
            searchTime = Environment.TickCount - startTime;
        }

        public void quick_Sort(int[] cloneArray, out int comparisons, out int searchTime, out int swaps)
        {
            int comps = 0;
            int swapCount = 0;

            int startTime = Environment.TickCount;
            void ReqursiveQuickSort(int leftBound, int rightBound)
            {
                if (leftBound < rightBound)
                {
                    int pivot = cloneArray[leftBound];
                    int i = leftBound - 1;
                    int j = rightBound + 1;
                    do
                    {
                        do
                        {
                            i++;
                            comps++;
                        } while (cloneArray[i] < pivot);
                        do
                        {
                            j--;
                            comps++;
                        } while (cloneArray[j] > pivot);
                        if (i < j)
                        {
                            (cloneArray[i], cloneArray[j]) = (cloneArray[j], cloneArray[i]);
                            swapCount++;
                        }
                    } while (i < j);
                    ReqursiveQuickSort(leftBound, j);
                    ReqursiveQuickSort(j + 1, rightBound);
                }
            }
            ReqursiveQuickSort(0, cloneArray.Length - 1);
            searchTime = Environment.TickCount - startTime;

            comparisons = comps;
            swaps = swapCount;
        }

        public void shell_Sort(int[] cloneArray, out int comparisons, out int searchTime, out int swaps)
        {
            comparisons = 0;
            swaps = 0;
            int n = cloneArray.Length;

            int startTime = Environment.TickCount;

            int step = (int)Math.Pow(2, (int)Math.Log(n, 2) - 1) - 1;
            while (step > 0)
            {
                for (int i = step; i < n; i++)
                {
                    int current = cloneArray[i];
                    int j = i;
                    while (j >= step && cloneArray[j - step] > current)
                    {
                        cloneArray[j] = cloneArray[j - step];
                        j -= step;
                        swaps++;
                        comparisons++;
                    }
                    comparisons++;
                    cloneArray[j] = current;
                    swaps++;
                }

                step /= 2;
            }

            searchTime = Environment.TickCount - startTime;
        }


        public bool is_Sorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            int arraySize = (int)numericUpDown1.Value;
            generate_Array(arraySize);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value == null) continue;

                bool isSelected = Convert.ToBoolean(row.Cells[0].Value);

                if (isSelected)
                {
                    string sortName = row.Cells[1].Value.ToString();
                    int[] cloneArray = (int[])array.Clone();

                    int comps = 0, searchTime = 0, swapCount = 0;

                    switch (sortName)
                    {
                        case "Обмен":
                            bubble_Sort(cloneArray, out comps, out searchTime, out swapCount);
                            break;
                        case "Выбор":
                            choice_Sort(cloneArray, out comps, out searchTime, out swapCount);
                            break;
                        case "Включение":
                            inclusion_Sort(cloneArray, out comps, out searchTime, out swapCount);
                            break;
                        case "Быстрая":
                            quick_Sort(cloneArray, out comps, out searchTime, out swapCount);
                            break;
                        case "Шелла":
                            shell_Sort(cloneArray, out comps, out searchTime, out swapCount);
                            break;
                        default:
                            break;
                    }
                    
                    row.Cells[2].Value = comps;
                    row.Cells[3].Value = swapCount;
                    row.Cells[4].Value = searchTime;
                    row.Cells[5].Value = is_Sorted(cloneArray) ? "Да" : "Нет";
                } else
                {
                    row.Cells[2].Value = null;
                    row.Cells[3].Value = null;
                    row.Cells[4].Value = null;
                    row.Cells[5].Value = null;
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
