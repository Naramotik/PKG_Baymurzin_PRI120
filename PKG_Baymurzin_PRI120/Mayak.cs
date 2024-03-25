using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace PKG_Baymurzin_PRI120
{
    public class Mayak
    {

        private const float a = 1;
        private const float b = 9;
        private const float n = 30;
        private double[,] GeometricArray = new double[30, 3];



        private float rot_1, rot_2;
        private double[,,] ResaultGeometric = new double[64, 64, 3];
        private int count_elements = 0;
        private double Angle = 2 * Math.PI / 64;
        private int Iter = 64;

        public Mayak() { }

        public void drawMayak()
        {
            // МАЯК
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(10, 100, 0);
            Gl.glVertex3d(15, 105, 45);
            Gl.glVertex3d(25, 95, 45);
            Gl.glVertex3d(25, 90, 0);


            Gl.glVertex3d(25, 90, 0);
            Gl.glVertex3d(40, 100, 0);
            Gl.glVertex3d(35, 105, 45);
            Gl.glVertex3d(25, 95, 45);


            Gl.glVertex3d(40, 100, 0);
            Gl.glVertex3d(40, 115, 0);
            Gl.glVertex3d(35, 110, 45);
            Gl.glVertex3d(35, 105, 45);

            Gl.glVertex3d(40, 115, 0);
            Gl.glVertex3d(25, 120, 0);
            Gl.glVertex3d(25, 115, 45);
            Gl.glVertex3d(35, 110, 45);

            Gl.glVertex3d(25, 120, 0);
            Gl.glVertex3d(10, 115, 0);
            Gl.glVertex3d(15, 110, 45);
            Gl.glVertex3d(25, 115, 45);

            Gl.glVertex3d(10, 115, 0);
            Gl.glVertex3d(10, 100, 0);
            Gl.glVertex3d(15, 105, 45);
            Gl.glVertex3d(15, 110, 45);



            Gl.glEnd();
            Gl.glPopMatrix();
        }


        public void drawTop()
        {
            // ВЕРХУШКА МАЯКА
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);


            Gl.glVertex3d(15, 105, 45);
            Gl.glVertex3d(15, 105, 57);
            Gl.glVertex3d(25, 95, 57);
            Gl.glVertex3d(25, 95, 45);

            Gl.glVertex3d(25, 95, 45);
            Gl.glVertex3d(25, 95, 57);
            Gl.glVertex3d(35, 105, 57);
            Gl.glVertex3d(35, 105, 45);

            Gl.glVertex3d(35, 110, 45);
            Gl.glVertex3d(35, 110, 57);
            Gl.glVertex3d(35, 105, 57);
            Gl.glVertex3d(35, 105, 45);

            Gl.glVertex3d(35, 110, 45);
            Gl.glVertex3d(35, 110, 57);
            Gl.glVertex3d(25, 115, 57);
            Gl.glVertex3d(25, 115, 45);

            Gl.glVertex3d(15, 110, 45);
            Gl.glVertex3d(15, 110, 57);
            Gl.glVertex3d(25, 115, 57);
            Gl.glVertex3d(25, 115, 45);

            Gl.glVertex3d(15, 105, 45);
            Gl.glVertex3d(15, 105, 57);
            Gl.glVertex3d(15, 110, 57);
            Gl.glVertex3d(15, 110, 45);



            Gl.glEnd();
            Gl.glPopMatrix();
        }

        public void drawRoof()
        {
            // Крыша МАЯКА
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_TRIANGLES);


            Gl.glVertex3d(15, 105, 57);
            Gl.glVertex3d(25, 95, 57);
            Gl.glVertex3d(25, 105, 65);

            Gl.glVertex3d(25, 95, 57);
            Gl.glVertex3d(35, 105, 57);
            Gl.glVertex3d(25, 105, 65);

            Gl.glVertex3d(35, 110, 57);
            Gl.glVertex3d(35, 105, 57);
            Gl.glVertex3d(25, 105, 65);


            Gl.glVertex3d(35, 110, 57);
            Gl.glVertex3d(25, 115, 57);
            Gl.glVertex3d(25, 105, 65);


            Gl.glVertex3d(15, 110, 57);
            Gl.glVertex3d(25, 115, 57);
            Gl.glVertex3d(25, 105, 65);

            Gl.glVertex3d(15, 105, 57);
            Gl.glVertex3d(15, 110, 57);
            Gl.glVertex3d(25, 105, 65);



            Gl.glEnd();
            Gl.glPopMatrix();
        }


        public void drawDoor()
        {
            // Дверка маяка
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(15, 96.3f, 0);
            Gl.glVertex3d(15, 96.3f, 15);
            Gl.glVertex3d(20, 93, 15);
            Gl.glVertex3d(20, 93, 0);



            Gl.glEnd();
            Gl.glPopMatrix();
        }

        public void drawHandle()
        {
            // Дверка маяка
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(15, 96f, 6);
            Gl.glVertex3d(15, 96f, 7);
            Gl.glVertex3d(16, 95, 7);
            Gl.glVertex3d(16, 95, 6);



            Gl.glEnd();
            Gl.glPopMatrix();
        }







        public void drawLight()
        {
            // количество элементов последовательности геометрии, на основе которых будет строится тело вращения
            count_elements = 30;
            // непосредственное заполнение точек
            // после изменения данной геометрии мы сразу получим новое тело вращения

            InitGeometricArray();

            for (int ax = 0; ax < count_elements; ax++)
            {

                // цикл по меридианам объекта, заранее определенным в программе
                for (int bx = 0; bx < Iter; bx++)
                {

                    // для всех (bx > 0) элементов алгоритма используются предыдушая построенная последовательность
                    // для ее поворота на установленный угол
                    if (bx > 0)
                    {

                        double new_x = ResaultGeometric[ax, bx - 1, 0] * Math.Cos(Angle) - ResaultGeometric[ax, bx - 1, 1] * Math.Sin(Angle);
                        double new_y = ResaultGeometric[ax, bx - 1, 0] * Math.Sin(Angle) + ResaultGeometric[ax, bx - 1, 1] * Math.Cos(Angle);
                        ResaultGeometric[ax, bx, 0] = new_x;
                        ResaultGeometric[ax, bx, 1] = new_y;
                        ResaultGeometric[ax, bx, 2] = GeometricArray[ax, 2];

                    }
                    else // для построения первого меридиана мы используем начальную кривую, описывая ее нулевым значением угла поворота
                    {

                        double new_x = GeometricArray[ax, 0] * Math.Cos(0) - GeometricArray[ax, 1] * Math.Sin(0);
                        double new_y = GeometricArray[ax, 1] * Math.Sin(0) + GeometricArray[ax, 1] * Math.Cos(0);
                        ResaultGeometric[ax, bx, 0] = new_x;
                        ResaultGeometric[ax, bx, 1] = new_y;
                        ResaultGeometric[ax, bx, 2] = GeometricArray[ax, 2];

                    }
                }

            }







        }



        private void InitGeometricArray()
        {
            for (float i = a, counter = 0; counter < n; i += (b - a) / n, counter++)
            {
                GeometricArray[(int)counter, 0] = i;
                GeometricArray[(int)counter, 1] = 2 * Math.Sin(i) + 1.5 * Math.Sqrt(i);
                GeometricArray[(int)counter, 2] = i;

            }
        }


        public void draw()
        {

            // два параметра, которые мы будем использовать для непрерывного вращения сцены вокруг двух координатных осей
            rot_1 = rot_1 + 5;
            rot_2++; // очистка буфера цвета и буфера глубины


            // установка положения камеры (наблюдателя). Как видно из кода
            // дополнительно на положение наблюдателя по оси Z влияет значение,
            // установленное в ползунке, доступном для пользователя


            // 2 поворота 
            Gl.glRotated(90, 1.0, 0.0, 0.0);
            Gl.glRotated(rot_1++, 0.0, 20.0, 0.0);
            Gl.glScalef(1.4f, 1.4f, 4.5f);

            Gl.glBegin(Gl.GL_QUADS); // режим отрисовки полигонов, состоящих из 4 вершин
            for (int ax = 0; ax < count_elements; ax++)
            {

                for (int bx = 0; bx < Iter; bx++)
                {

                    // вспомогательные переменные для более наглядного использования кода при расчете нормалей
                    double x1 = 0, x2 = 0, x3 = 0, x4 = 0, y1 = 0, y2 = 0, y3 = 0, y4 = 0, z1 = 0, z2 = 0, z3 = 0, z4 = 0;

                    // первая вершина
                    x1 = ResaultGeometric[ax, bx, 0];
                    y1 = ResaultGeometric[ax, bx, 1];
                    z1 = ResaultGeometric[ax, bx, 2];

                    if (ax + 1 < count_elements) // если текущий ax не последний
                    {

                        // берем следующую точку последовательности
                        x2 = ResaultGeometric[ax + 1, bx, 0];
                        y2 = ResaultGeometric[ax + 1, bx, 1];
                        z2 = ResaultGeometric[ax + 1, bx, 2];

                        if (bx + 1 < Iter - 1) // если текущий bx не последний
                        {

                            // берем следующую точку последовательности и следующий меридиан
                            x3 = ResaultGeometric[ax + 1, bx + 1, 0];
                            y3 = ResaultGeometric[ax + 1, bx + 1, 1];
                            z3 = ResaultGeometric[ax + 1, bx + 1, 2];

                            // точка, соотвествующуя по номеру только на соседнем меридиане
                            x4 = ResaultGeometric[ax, bx + 1, 0];
                            y4 = ResaultGeometric[ax, bx + 1, 1];
                            z4 = ResaultGeometric[ax, bx + 1, 2];

                        }
                        else
                        {

                            // если это последний меридиан, то в качестве следующего мы берем начальный (замыкаем геометрию фигуры)
                            x3 = ResaultGeometric[ax + 1, 0, 0];
                            y3 = ResaultGeometric[ax + 1, 0, 1];
                            z3 = ResaultGeometric[ax + 1, 0, 2];

                            x4 = ResaultGeometric[ax, 0, 0];
                            y4 = ResaultGeometric[ax, 0, 1];
                            z4 = ResaultGeometric[ax, 0, 2];

                        }
                    }
                    else // данный элемент ax последний, следовательно мы будем использовать начальный (нулевой) вместо данного ax
                    {

                        // слудуещей точкой будет нулевая ax
                        x2 = ResaultGeometric[0, bx, 0];
                        y2 = ResaultGeometric[0, bx, 1];
                        z2 = ResaultGeometric[0, bx, 2];


                        if (bx + 1 < Iter - 1)
                        {

                            x3 = ResaultGeometric[0, bx + 1, 0];
                            y3 = ResaultGeometric[0, bx + 1, 1];
                            z3 = ResaultGeometric[0, bx + 1, 2];

                            x4 = ResaultGeometric[ax, bx + 1, 0];
                            y4 = ResaultGeometric[ax, bx + 1, 1];
                            z4 = ResaultGeometric[ax, bx + 1, 2];

                        }
                        else
                        {

                            x3 = ResaultGeometric[0, 0, 0];
                            y3 = ResaultGeometric[0, 0, 1];
                            z3 = ResaultGeometric[0, 0, 2];

                            x4 = ResaultGeometric[ax, 0, 0];
                            y4 = ResaultGeometric[ax, 0, 1];
                            z4 = ResaultGeometric[ax, 0, 2];

                        }

                    }


                    // переменные для расчета нормали
                    double n1 = 0, n2 = 0, n3 = 0;

                    // нормаль будем расчитывать как векторное произведение граней полигона
                    // для нулевого элемента нормаль мы будем считать немного по-другому

                    // на самом деле разница в расчете нормали актуальна только для последнего и первого полигона на меридиане

                    if (ax == 0) // при расчете нормали для ax мы будем использовать точки 1,2,3
                    {

                        n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                        n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                        n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    }
                    else // для остальных - 1,3,4
                    {

                        n1 = (y4 - y3) * (z1 - z3) - (y1 - y3) * (z4 - z3);
                        n2 = (z4 - z3) * (x1 - x3) - (z1 - z3) * (x4 - x3);
                        n3 = (x4 - x3) * (y1 - y3) - (x1 - x3) * (y4 - y3);

                    }


                    // если не включен режим GL_NORMILIZE, то мы должны в обязательном порядке
                    // произвести нормализацию вектора нормали, перед тем как передать информацию о нормали
                    double n5 = (double)Math.Sqrt(n1 * n1 + n2 * n2 + n3 * n3);
                    n1 /= (n5 + 0.01);
                    n2 /= (n5 + 0.01);
                    n3 /= (n5 + 0.01);

                    // передаем информацию о нормали
                    Gl.glNormal3d(-n1, -n2, -n3);

                    // передаем 4 вершины для отрисовки полигона
                    Gl.glVertex3d(x1, y1, z1);
                    Gl.glVertex3d(x2, y2, z2);
                    Gl.glVertex3d(x3, y3, z3);
                    Gl.glVertex3d(x4, y4, z4);

                }

            }

            // завершаем выбранный режим рисования полигонов
            Gl.glEnd();

        }
    }
}

