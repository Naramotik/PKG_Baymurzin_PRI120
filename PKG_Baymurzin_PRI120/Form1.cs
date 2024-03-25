using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace PKG_Baymurzin_PRI120
{
    public partial class Form1 : Form
    {
        double a = 0, b = -0.575, c = -8.5, d = -61, zoom = 0.6;

        private float[,] camera_date = new float[5, 7];
        double translateX = 0, translateY = 0, translateZ = 0;
        bool isLighting, night = false;
        int frctl = 0;
        string lightColor = "white";

        string defoTexture = "defo.png";
        uint defoSign;
        int imageId;

        string ctulhuTexture = "ctulhu.png";
        uint ctulhuSign;
        float ctulhu_move = -17;
        float ctulhu_move_incrementator = 0.5f;

        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();


        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            // переводим значение, установившееся в элементе trackBar, в необходимый нам формат
            a = (double)trackBar4.Value;
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                translateY -= cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                translateY += cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                translateX += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                translateX -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                translateZ += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }
            if (e.KeyCode == Keys.D1)
            {
                lightColor = "white";
            }
            if (e.KeyCode == Keys.D2)
            {
                lightColor = "light-yellow";
            }
            if (e.KeyCode == Keys.D3)
            {
                lightColor = "yellow";
            }
            if (e.KeyCode == Keys.D4)
            {
                lightColor = "orange";
            }
        }



        double cameraSpeed;
        Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            isLighting = !isLighting;

        }

        float global_time = 0;
        int tick_count = 0;

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            global_time += (float)RenderTimer.Interval / 1000;
            Draw();

            ctulhu_move = ctulhu_move + ctulhu_move_incrementator;

            if (tick_count == 0)
            {
                BOOOOM_2.Boooom(global_time);
                BOOOOM_3.Boooom(global_time);
                BOOOOM_6.Boooom(global_time);
            }

            if (tick_count == 20)
            {
                BOOOOM_1.Boooom(global_time);
                BOOOOM_4.Boooom(global_time);
                BOOOOM_5.Boooom(global_time);

            }

            if (tick_count < 40)
            {
                tick_count++;
            } else
            {
                if (ctulhu_move_incrementator == 0.5f)
                    ctulhu_move_incrementator = -0.5f;
                else
                    ctulhu_move_incrementator = 0.5f;


                tick_count = 0;
                if (isLighting)
                {
                    switch (lightColor)
                    {
                        case "yellow":
                            lightColor = "orange";
                            break;
                        case "light-yellow":
                            lightColor = "yellow";
                            break;
                        case "white":
                            lightColor = "light-yellow";
                            break;

                    }
                }

            }
        }


        class RGB
        {
            private float R;
            private float G;
            private float B;

            public RGB(float R, float G, float B)
            {
                this.R = R;
                this.G = G;
                this.B = B;
            }

            public float getR()
            {
                return R;
            }

            public float getG()
            {
                return G;
            }

            public float getB()
            {
                return B;
            }
        }



        float deltaColor = 0;

        private void setColor(float R, float G, float B)
        {
            RGB color = new RGB(R - deltaColor, G - deltaColor, B - deltaColor);
            Gl.glColor3f(color.getR(), color.getG(), color.getB());
        }

        public void setDeltaColor(float delta)
        {
            deltaColor = delta;
        }



        Mayak mayak = new Mayak();

        private void button2_Click(object sender, EventArgs e)
        {

            if (night)
            {
                Gl.glEnable(Gl.GL_LIGHTING);
                Gl.glClearColor(0, 0, 0, 1);
            } else
            {
                Gl.glDisable(Gl.GL_LIGHTING);
                Gl.glClearColor(255, 255, 255, 1);
            }
            night = !night;
        }

        private float rot_1, rot_2;

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Info = new Info();
            Info.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
                                                                                
            Gl.glNormal3f(0, 0, 1);                                                 // Нормаль для освещения

            rot_1++;

            // Установка камеры
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            // в зависимсоти от установленного режима отрисовываем сцену в черном или белом цвете
            int camera = comboBox3.SelectedIndex;
            // производим перемещение в зависимости от значений, полученных при перемещении ползунков
            Gl.glTranslated(camera_date[camera, 0] + translateX, camera_date[camera, 1] + translateY, camera_date[camera, 2] + translateZ);
            Gl.glRotated(camera_date[camera, 3], camera_date[camera, 4] + a / 500, camera_date[camera, 5] + a / 500, camera_date[camera, 6] + a / 500);
            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);





            // ОСТРОВ
            Gl.glPushMatrix();
            setColor(0.900f, 0.890f, 0.500f);
            Gl.glBegin(Gl.GL_QUADS);
            //Поверхность острова
            Gl.glVertex3d(-50, 0, 0);
            Gl.glVertex3d(-50, 50, 0);
            Gl.glVertex3d(50, 50, 0);
            Gl.glVertex3d(50, 0, 0);

            Gl.glVertex3d(0, 50, 0);
            Gl.glVertex3d(0, 125, 0);
            Gl.glVertex3d(50, 125, 0);
            Gl.glVertex3d(50, 50, 0);

            //Бортики острова
            setColor(0.600f, 0.590f, 0.200f);
            Gl.glVertex3d(-50, 0, -3);
            Gl.glVertex3d(-50, 0, 0);
            Gl.glVertex3d(50, 0, 0);
            Gl.glVertex3d(50, 0, -3);

            Gl.glVertex3d(-50, 0, 0);
            Gl.glVertex3d(-50, 0, -3);
            Gl.glVertex3d(-50, 50, -3);
            Gl.glVertex3d(-50, 50, 0);

            Gl.glVertex3d(0, 50, 0);
            Gl.glVertex3d(0, 50, -3);
            Gl.glVertex3d(0, 125, -3);
            Gl.glVertex3d(0, 125, 0);

            Gl.glVertex3d(-50, 50, 0);
            Gl.glVertex3d(-50, 50, -3);
            Gl.glVertex3d(0, 50, -3);
            Gl.glVertex3d(0, 50, 0);

            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();



            // МОРЕ
            Gl.glPushMatrix();
            setColor(0.100f, 0.390f, 0.600f);
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(-400, -400, -2);
            Gl.glVertex3d(-400, 600, -2);
            Gl.glVertex3d(400, 600, -2);
            Gl.glVertex3d(400, -400, -2);

            Gl.glEnd();
            Gl.glPopMatrix();



            // МАЯК
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);

            // Основание маяка
            setColor(0.3f, 0.3f, 0.3f);
            mayak.drawMayak();

            // Верхушка
            setColor(0.6f, 0.1f, 0.1f);
            mayak.drawTop();

            // Крыша
            setColor(0.5f, 0.0f, 0.0f);
            mayak.drawRoof();

            // Дверь
            setColor(0.6f, 0.4f, 0.0f);
            mayak.drawDoor();

            // Ручка двери
            setColor(0.4f, 0.2f, 0.0f);
            mayak.drawHandle();

            Gl.glEnd();
            Gl.glPopMatrix();






            // Картина над дверью (для текстуры)

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, defoSign);

            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(17.3, 96.3f, 20);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(17.5, 97.5f, 30);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(21.4, 95.8, 30);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(22, 94.4, 20);
            Gl.glTexCoord2f(0, 0);

            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);





            // Ctulhu
            if (!isLighting && !night)
            {
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, ctulhuSign);


                Gl.glPushMatrix();

                Gl.glTranslated(0, 0, ctulhu_move);

                Gl.glBegin(Gl.GL_QUADS);



                Gl.glVertex3d(-80, 250, -30);
                Gl.glTexCoord2f(0, 1);
                Gl.glVertex3d(-80, 250, 70);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex3d(20, 250, 70);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex3d(20, 250, -30);
                Gl.glTexCoord2f(0, 0);

                Gl.glEnd();
                Gl.glPopMatrix();
                Gl.glDisable(Gl.GL_TEXTURE_2D);
            }



            setColor(0f, 0.2f, 0f);
            // Фрактал #1 (передний)
            Gl.glPushMatrix();

            Gl.glTranslated(-27, 50, 2);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScalef(0.2f, 0.05f, 0.1f);

            Gl.glBegin(Gl.GL_LINES);
            drawLevyFractal(-60, -5, 60, -5, 16);
            Gl.glEnd();
            Gl.glPopMatrix();

            // Фрактал #2 (левый борт)
            Gl.glPushMatrix();

            Gl.glTranslated(-50, 25, 2);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(180, 0, 1, 0);
            Gl.glScalef(0.2f, 0.05f, 0.1f);

            Gl.glBegin(Gl.GL_LINES);
            drawLevyFractal(-65, -5, 65, -5, 16);
            Gl.glEnd();
            Gl.glPopMatrix();




            // Ножка угловая
            Gl.glPushMatrix();
            setColor(0f, 0.2f, 0f);
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(-50, 49, 0);
            Gl.glVertex3d(-50, 49, 8);
            Gl.glVertex3d(-49, 49, 8);
            Gl.glVertex3d(-49, 49, 0);

            Gl.glVertex3d(-49, 50, 0);
            Gl.glVertex3d(-49, 50, 8);
            Gl.glVertex3d(-49, 49, 8);
            Gl.glVertex3d(-49, 49, 0);

            Gl.glVertex3d(-50, 50, 0);
            Gl.glVertex3d(-50, 50, 8);
            Gl.glVertex3d(-50, 49, 8);
            Gl.glVertex3d(-50, 49, 0);

            Gl.glVertex3d(-50, 50, 0);
            Gl.glVertex3d(-50, 50, 8);
            Gl.glVertex3d(-49, 50, 8);
            Gl.glVertex3d(-49, 50, 0);

            Gl.glEnd();
            Gl.glPopMatrix();


            // Ножка Правая
            Gl.glPushMatrix();
            setColor(0f, 0.2f, 0f);
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(-4, 49, 0);
            Gl.glVertex3d(-4, 49, 8);
            Gl.glVertex3d(-3, 49, 8);
            Gl.glVertex3d(-3, 49, 0);

            Gl.glVertex3d(-3, 50, 0);
            Gl.glVertex3d(-3, 50, 8);
            Gl.glVertex3d(-3, 49, 8);
            Gl.glVertex3d(-3, 49, 0);

            Gl.glVertex3d(-4, 50, 0);
            Gl.glVertex3d(-4, 50, 8);
            Gl.glVertex3d(-4, 49, 8);
            Gl.glVertex3d(-4, 49, 0);

            Gl.glVertex3d(-4, 50, 0);
            Gl.glVertex3d(-4, 50, 8);
            Gl.glVertex3d(-3, 50, 8);
            Gl.glVertex3d(-3, 50, 0);

            Gl.glEnd();
            Gl.glPopMatrix();

            // Ножка Левая
            Gl.glPushMatrix();
            setColor(0f, 0.2f, 0f);
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(-50, 0, 0);
            Gl.glVertex3d(-50, 0, 8);
            Gl.glVertex3d(-49, 0, 8);
            Gl.glVertex3d(-49, 0, 0);

            Gl.glVertex3d(-49, 1, 0);
            Gl.glVertex3d(-49, 1, 8);
            Gl.glVertex3d(-49, 0, 8);
            Gl.glVertex3d(-49, 0, 0);

            Gl.glVertex3d(-50, 1, 0);
            Gl.glVertex3d(-50, 1, 8);
            Gl.glVertex3d(-50, 0, 8);
            Gl.glVertex3d(-50, 0, 0);

            Gl.glVertex3d(-50, 1, 0);
            Gl.glVertex3d(-50, 1, 8);
            Gl.glVertex3d(-49, 1, 8);
            Gl.glVertex3d(-49, 1, 0);

            Gl.glEnd();
            Gl.glPopMatrix();





            // Капли 
            setColor(0.0f, 0.7f, 1f);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);

            BOOOOM_1.Calculate(global_time);
            BOOOOM_2.Calculate(global_time);
            BOOOOM_3.Calculate(global_time);
            BOOOOM_4.Calculate(global_time);

            BOOOOM_5.Calculate(global_time);
            BOOOOM_6.Calculate(global_time);
            Gl.glEnd();
            Gl.glPopMatrix();


            float r = 0;
            float g = 0;
            float b = 0;


            // Отрисовка света маяка
            if (isLighting)
            {
                switch (lightColor)
                {
                    case "orange":
                        setColor(0.7f, 0.6f, 0f);
                        r = 0.7f; g = 0.6f; b = 0;
                        break;
                    case "yellow":
                        setColor(0.99f, 0.99f, 0f);
                        r = 0.75f; g = 0.75f; b = 0;
                        break;
                    case "light-yellow":
                        setColor(0.99f, 0.99f, 0.5f);
                        r = 0.85f; g = 0.85f; b = 0.5f;
                        break;
                    case "white":
                        setColor(1f, 1f, 1f);
                        r = 1; g = 1; b = 1;
                        break;

                }

                mayak.drawLight();
                Gl.glTranslated(24, 106, 50);
                mayak.draw();
            }



            float[] position = { 0, 0, 5, 2 };
            float[] position2 = { 0, 0, 0, 2 };
            float[] direction = { 0, 1, 0 };
            float[] ambient = { r, g, b, 1 };
            float[] ambient2 = { r, g, b, 1 };


            Gl.glPushMatrix();
            Gl.glRotatef(rot_1++, 0, 0, 1);


            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, position);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPOT_DIRECTION, direction);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, ambient2);
            Gl.glPopMatrix();


            Gl.glPushMatrix();
            Gl.glRotatef(rot_1++, 0, 0, 1);

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, direction);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, ambient);
            Gl.glPopMatrix();


            //Завершающие данные
            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }



        // ФУНКЦИЯ ОТРИСОВКИ ФРАКТАЛА
        void drawLevyFractal(int x1, int y1, int x2, int y2, int i)
        {
            if (i == 0)
            {

                Gl.glVertex2i(x1, y1); //координаты вырисовываемого 
                Gl.glVertex2i(x2, y2); //отрезка
            }
            else
            {
                int x3 = (x1 + x2) / 2 - (y2 - y1) / 2; //координаты
                int y3 = (y1 + y2) / 2 + (x2 - x1) / 2; //точки излома
                drawLevyFractal(x1, y1, x3, y3, i - 1);
                drawLevyFractal(x3, y3, x2, y2, i - 1);
            }
        }

        // Капли
        private Explosion BOOOOM_1 = new Explosion(-3, -123, 0, 8, 120);
        private Explosion BOOOOM_2 = new Explosion(-3, -93, 0, 8, 120);

        private Explosion BOOOOM_3 = new Explosion(-3, -80, 0, 8, 50);
        private Explosion BOOOOM_4 = new Explosion(-3, -60, 0, 8, 50);

        private Explosion BOOOOM_5 = new Explosion(-50, -55, 1, 2, 50);
        private Explosion BOOOOM_6 = new Explosion(-20, -52, 1, 2, 50);


        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация openGL (glut)
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // цвет очистки окна
            Gl.glClearColor(0, 0, 0, 1);

            // настройка порта просмотра
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(60, (float)AnT.Width / (float)AnT.Height, 0.1, 900);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            comboBox3.SelectedIndex = 0;
            cameraSpeed = 1;

            RenderTimer.Start();



            // Включение освещения
            Gl.glEnable(Gl.GL_LIGHTING);

            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_NORMALIZE);

            // Генерация картины дефо
            defoSign = genImage(defoTexture);
            ctulhuSign = genImage(ctulhuTexture);



            //подключение звука
            WMP.URL = @"sea.mp3";
            WMP.controls.play();


            label5.BackColor = System.Drawing.Color.Transparent;




            //Сверху на маяк
            camera_date[0, 0] = -35;
            camera_date[0, 1] = -35;
            camera_date[0, 2] = -60;
            camera_date[0, 3] = -70;
            camera_date[0, 4] = 1f;
            camera_date[0, 5] = 0.4f;
            camera_date[0, 6] = 0.7f;

            // Забор и волны:
            camera_date[1, 0] = -15;
            camera_date[1, 1] = 2;
            camera_date[1, 2] = -75;
            camera_date[1, 3] = 180;
            camera_date[1, 4] = 0f;
            camera_date[1, 5] = 4.5f;
            camera_date[1, 6] = 5;


            // Ктулху:
            camera_date[2, 0] = 20;
            camera_date[2, 1] = -10;
            camera_date[2, 2] = 40;
            camera_date[2, 3] = -90;
            camera_date[2, 4] = 1f;
            camera_date[2, 5] = 0;
            camera_date[2, 6] = 0;


            // Остров:
            camera_date[3, 0] = -10;
            camera_date[3, 1] = -10;
            camera_date[3, 2] = 5;
            camera_date[3, 3] = -90;
            camera_date[3, 4] = 1f;
            camera_date[3, 5] = 0;
            camera_date[3, 6] = 0;
        }



        private uint genImage(string image)
        {
            uint sign = 0;
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage(image))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        sign = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        sign = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId);
            return sign;
        }

        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint texObject;
            Gl.glGenTextures(1, out texObject);
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glAlphaFunc(Gl.GL_GREATER, 0.1f);

            return texObject;
        }


    }
}
