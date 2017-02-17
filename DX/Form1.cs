﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.OpenGl;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DX
{
    public partial class Form1 : Form
    {
        Random RNG;

        Dictionary<string, int> Textures;
        Dictionary<int, Enemy> EnemyList;
        List<Item> DropList;
        List<Player> AllPlayers;

        int[,] map;
        int[,] obj_map;

        //float x, y;
        

        float ScrH, ScrW;
        int MouseX, MouseY;
        float MouseOnMatrixX, MouseOnMatrixY;

        bool QuestMenu = false;
        bool InvMenu = false;

        Player player;



        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Gl.glClearDepth(.5);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();


            Gl.glPushMatrix();
            Gl.glTranslatef(-player.X + ScrW / 2, -player.Y + ScrH / 2, 0);



            // Отрисовка окружения


            for (int i = (int)player.X-8; i < player.X + 8; i ++) {
                for (int j = (int)player.Y - 5; j < player.Y + 5; j++)
                {
                    if (!player.Alive) Gl.glColor3f(.5f, .5f, .5f);
                    else Gl.glColor3f(1f, 1f, 1f);
                    //Текстуры карты
                    if (i < 0 || j < 0 || i > map.GetUpperBound(0) || j > map.GetUpperBound(1)) continue;
                    if (map[i, j] == 0) Draw2DText(i, j, 0, 1f, 1f, Textures["Grass"]);
                    if (map[i, j] == 1) Draw2DText(i, j, 0, 1f, 1f, Textures["Ice"]);
                    if (map[i, j] == 2) Draw2DText(i, j, 0, 1f, 1f, Textures["Sand"]);
                    if (map[i, j] == 3) Draw2DText(i, j, 0, 1f, 1f, Textures["Ground"]);
                    if (map[i, j] == 4) Draw2DText(i, j, 0, 1f, 1f, Textures["StWall"]);
                    if (map[i, j] == 5) Draw2DText(i, j, -1.5f, 1f, 1f, Textures["StBlack"]);
                    if (map[i, j] == 6) Draw2DText(i, j, 0, 1f, 1f, Textures["StFloor"]);
                    if (map[i, j] == 7) Draw2DText(i, j, 0, 1f, 1f, Textures["WFloor"]);
                    if (map[i, j] == 8) Draw2DText(i, j, -1.5f, 1f, 1f, Textures["StBlack"]);

                    //Текстуры объектов
                    if (obj_map[i, j] == 1) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadD"]);
                    if (obj_map[i, j] == 2) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadL"]);
                    if (obj_map[i, j] == 3) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadLD"]);
                    if (obj_map[i, j] == 4) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadLU"]);
                    if (obj_map[i, j] == 5) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadR"]);
                    if (obj_map[i, j] == 6) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadRD"]);
                    if (obj_map[i, j] == 7) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadRU"]);
                    if (obj_map[i, j] == 8) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadU"]);
                    if (obj_map[i, j] == 9) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadZDL"]);
                    if (obj_map[i, j] == 10) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadZDR"]);
                    if (obj_map[i, j] == 11) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadZUL"]);
                    if (obj_map[i, j] == 12) Draw2DText(i, j, 0, 1f, 1f, Textures["RoadZUR"]);
                }
            }

            Gl.glTranslatef(player.X - ScrW / 2, player.Y - ScrH / 2, 0);
            Gl.glPopMatrix();


            //отрисовка героя

            //проверка жизни

            //
            //

            MousePosOnAnt(out MouseX, out MouseY, out MouseOnMatrixX, out MouseOnMatrixY);

            Gl.glColor3f(1,1,1);

            //
            //
            
            //label1.Text = player.Hp.ToString();
            label2.Text = "Px=" + player.X.ToString() + "\tPy=" + player.Y;

            Gl.glPushMatrix();

            Gl.glTranslatef(ScrW / 2, ScrH / 2, 0);

            if (player.Alive)
            {
                
                if (!player.LEFT && !player.RIGHT && !player.UP && !player.DOWN && !player.Attack)
                {
                    player.ResetAnim();
                    if (player.Rotation < 45 && player.Rotation >= -45) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["IdleR"]);
                    if (player.Rotation >= 45 && player.Rotation < 135) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["IdleU"]);
                    if (player.Rotation >= 135 || player.Rotation < -135) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["IdleL"]);
                    if (player.Rotation >= -135 && player.Rotation < -45) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["IdleD"]);
                }


                if (player.Attack)
                {
                    if (player.Rotation < 45 && player.Rotation >= -45) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroAtkR" + player.AttackAtState.ToString()]);
                    if (player.Rotation >= 45 && player.Rotation < 135) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroAtkU" + player.AttackAtState.ToString()]);
                    if (player.Rotation >= 135 || player.Rotation < -135) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroAtkL" + player.AttackAtState.ToString()]);
                    if (player.Rotation >= -135 && player.Rotation < -45) Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroAtkD" + player.AttackAtState.ToString()]);
                }
                else
                {

                    if (player.LEFT)
                    {
                        Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroLEFT" + player.RunAtState.ToString()]);
                    }
                    if (player.UP)
                    {
                        Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroUP" + player.RunAtState.ToString()]);
                    }
                    if (player.DOWN)
                    {
                        Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroDOWN" + player.RunAtState.ToString()]);
                    }
                    if (player.RIGHT)
                    {
                        Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["HeroRIGHT" + player.RunAtState.ToString()]);
                    }
                }

            }else
            {
                Gl.glRotatef(90f,0,0,1);
                Draw2DTextCent(0, .60f, -1, 1.5f, 1.5f, Textures["IdleR"]);
            }
            Gl.glPopMatrix();



            //отрисовка противников
            Gl.glPushMatrix();
            Gl.glTranslatef(-player.X + ScrW / 2, -player.Y + ScrH / 2, 0);


            //
            //


            foreach (KeyValuePair<int, Enemy> enemy in GetNearbyEnemies(player.X, player.Y, EnemyList))
            {
                if (enemy.Value.Active)
                {
                    if (!player.Alive) Gl.glColor3f(.5f, .5f, .5f);
                    else Gl.glColor3f(1f, 1f, 1f);
                    //Gl.glClearDepth(1);
                    if (enemy.Value.Y - .1f < player.Y) Draw2DTextCent(enemy.Value.X, enemy.Value.Y + .5f, -2, 1.2f, 1.2f, Textures[enemy.Value.Texture]);
                    else Draw2DTextCent(enemy.Value.X, enemy.Value.Y + .5f, -.5f, 1.2f, 1.2f, Textures[enemy.Value.Texture]);
                    Gl.glColor3f(0f, 0f, 0f);
                    Draw2DText(enemy.Value.X - .6f, enemy.Value.Y + 1.25f, -3, .9f, .15f, Textures["HPBAR"]);
                    Gl.glColor3f(1f, 0f, 0f);
                    Draw2DText(enemy.Value.X - .6f, enemy.Value.Y + 1.25f, -3, .9f / enemy.Value.MaxHp * enemy.Value.HP, .15f, Textures["HPBAR"]);
                }
            }

            //отрисовка дропа

            foreach (Item item in GetNearbyItems(player.X, player.Y, DropList))
            {
                Gl.glColor3f(1, 1, 1);
                if (item.Y - .1f < player.Y) Draw2DTextCent(item.X, item.Y,-2,.4f,.4f,Textures[item.Texture]);
                else Draw2DTextCent(item.X, item.Y, -.5f, .4f, .4f, Textures[item.Texture]);
            }

            Gl.glTranslatef(player.X - ScrW / 2, player.Y - ScrH / 2, 0);
            Gl.glPopMatrix();



            //Отрисовка меню
            if (InvMenu)
            {
                Gl.glColor3f(1, .8f, 0);
                Draw2DText(1,2,-3,4,7,Textures["Menu"]);
                Gl.glLoadIdentity();
                DrawStringCent(1,5, 8.25f, -3.5f, Glut.GLUT_BITMAP_HELVETICA_18, "Inventory", 1f, 1f, 1f, true);
                for (int i = 0; i < player.Inventory.Size; i++) {
                    Gl.glColor3f(1, 1, 1);
                    Draw2DText(1.5f+i%player.Inventory.Width*3f/4f,
                               7-(i-i%player.Inventory.Width)/player.Inventory.Width * 3f / 4f,
                               -3.5f, 3f/4f,3f/4f,
                               Textures["ItemBG"]);
                    if (player.Inventory.Items[i] != null) {
                        Draw2DText(1.5f + i % player.Inventory.Width * 3f / 4f,
                                   7 - (i - i % player.Inventory.Width) / player.Inventory.Width * 3f / 4f,
                                   -3.5f, 3f / 4f, 3f / 4f,
                                   Textures[player.Inventory.Items[i].Texture]);
                        if (player.Inventory.Items[i].Quantity > 1||true) {
                            DrawString(2f + i % player.Inventory.Width * 3f / 4f,
                                   7 - (i - i % player.Inventory.Width) / player.Inventory.Width * 3f / 4f,
                                   -3.5f, Glut.GLUT_BITMAP_HELVETICA_18, player.Inventory.Items[i].Quantity.ToString(),
                                   1, 1, 1, true);
                        }
                    }
                }
                int X = (int)((MouseOnMatrixX - 1.5f) * 4f / 3f);
                int Y = (int)((7f + 3f / 4f - MouseOnMatrixY) * 4f / 3f);
                if (X >= 0 && X < player.Inventory.Width && Y >= 0 && Y < player.Inventory.Height && player.Inventory.Items[Y*player.Inventory.Width+X]!=null)
                {
                    
                    float Width = Glut.glutBitmapLength(Glut.GLUT_BITMAP_HELVETICA_18, player.Inventory.Items[Y * player.Inventory.Width + X].Name) * ScrW / AnT.Width
                        >= Glut.glutBitmapLength(Glut.GLUT_BITMAP_HELVETICA_12, player.Inventory.Items[Y * player.Inventory.Width + X].Desc) * ScrW / AnT.Width?
                        Glut.glutBitmapLength(Glut.GLUT_BITMAP_HELVETICA_18, player.Inventory.Items[Y * player.Inventory.Width + X].Name) * ScrW / AnT.Width:
                        Glut.glutBitmapLength(Glut.GLUT_BITMAP_HELVETICA_12, player.Inventory.Items[Y * player.Inventory.Width + X].Desc) * ScrW / AnT.Width;
                    float Height = Glut.glutBitmapHeight(Glut.GLUT_BITMAP_HELVETICA_12) * ScrH / AnT.Height;
                    int lines = 1;
                    for (int i = 0; i < player.Inventory.Items[Y * player.Inventory.Width + X].Desc.Length; i++) {
                        if (player.Inventory.Items[Y * player.Inventory.Width + X].Desc[i] == '\n') lines++;
                    }
                    Gl.glColor4f(.3f, .15f, 0,1);
                    Draw2DText(MouseOnMatrixX+.5f,MouseOnMatrixY-Height*lines-.4f,-4.5f,Width+.2f, Height*lines + .4f,Textures["DescBG"]);
                    //Gl.glColor3f(player.Inventory.Items[Y * player.Inventory.Width + X].NameR, player.Inventory.Items[Y * player.Inventory.Width + X].NameG, player.Inventory.Items[Y * player.Inventory.Width + X].NameB);
                    DrawString(MouseOnMatrixX+.6f,
                        MouseOnMatrixY  - .3f,
                        -5,
                        Glut.GLUT_BITMAP_HELVETICA_18,
                        player.Inventory.Items[Y * player.Inventory.Width + X].Name,
                        player.Inventory.Items[Y * player.Inventory.Width + X].NameR,
                        player.Inventory.Items[Y * player.Inventory.Width + X].NameG,
                        player.Inventory.Items[Y * player.Inventory.Width + X].NameB,true
                        );

                    DrawString(MouseOnMatrixX + .6f,
                        MouseOnMatrixY - .3f-Height,
                        -5,
                        Glut.GLUT_BITMAP_HELVETICA_12,
                        player.Inventory.Items[Y * player.Inventory.Width + X].Desc,.8f,.8f,.8f,
                        true
                        );
                }
            }

            if (QuestMenu)
            {
                Gl.glColor3f(1, .8f, 0);
                Draw2DText(11,2,-3,4,7,Textures["Menu"]);
                Gl.glLoadIdentity();
                DrawStringCent(11, 15, 8.25f, -3.5f, Glut.GLUT_BITMAP_HELVETICA_18, "Quests", 1f, 1f, 1f, true);
                if (player.Quests[0].State != 0)
                {
                    DrawStringCent(11, 15, 7.25f, -3.5f, Glut.GLUT_BITMAP_HELVETICA_18, player.Quests[0].Name, 1f, 1f, .5f, true);
                    DrawStringCent(11, 15, 6.75f, -3.5f, Glut.GLUT_BITMAP_HELVETICA_12, player.Quests[0].Desc[player.Quests[0].State - 1], 1f, 1f, .5f, true);
                }
            }


            // Отрисовка курсора и хп бара

            Gl.glColor4f(1, 1, 1,1);
            Draw2DText(MouseOnMatrixX, MouseOnMatrixY - .5f, -4, .5f, .5f,Textures["Cursor"]);
            Gl.glColor3f(0, 0, 0);
            Draw2DText(1, 1, -4, 4, .3f, Textures["HPBAR"]);
            Gl.glColor3f(1f, 0, 0);
            Draw2DText(1, 1, -4, 4*((float)player.Hp / (float)player.MaxHp) > 0 ? 4 * ((float)player.Hp / (float)player.MaxHp):0, .3f , Textures["HPBAR"]);


            Gl.glFlush();
            AnT.Invalidate();
        }

        private void LogicTimer_Tick(object sender, EventArgs e)
        {
            AllPlayers = new List<Player>();
            AllPlayers.Add(player);
            player.CheckDeath();
            player.CalcAnim();
            player.CalcRotation(MouseX, MouseY, AnT.Width / 2, AnT.Height / 2);

            foreach (KeyValuePair<int, Enemy> enemy in GetNearbyEnemies(player.X, player.Y, EnemyList))
            {


                enemy.Value.WorkCycle(AllPlayers);
                if (enemy.Value.DeathCheck()) {
                    enemy.Value.DropFunc(DropList, RNG);
                    EnemyList.Remove(enemy.Key);
                }
            }

            foreach (Player player in AllPlayers) {
                player.CheckQuests();
            }
        }

        public Form1()
        {

            InitializeComponent();
            AnT.InitializeContexts();
            Textures = new Dictionary<string, int>();
        }



        int LoadTexture(string name) //Функция загрузки текстуры
        {
            int texID;
            Gl.glGenTextures(1, out texID);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texID);

            var bmp = new Bitmap(name);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    int clr = bmp.GetPixel(i, j).ToArgb();
                    byte R = (byte)((clr >> 24) & 255);
                    byte G = (byte)((clr >> 16) & 255);
                    byte B = (byte)((clr >> 8) & 255);
                    byte A = (byte)(clr & 255);

                    byte[] inbuffer = BitConverter.GetBytes(clr);

                    byte wat=inbuffer[0];
                    inbuffer[0] = inbuffer[2];
                    inbuffer[2] = wat;

                    int outbuffer = BitConverter.ToInt32(inbuffer, 0);
                    
                    var clrout = Color.FromArgb(outbuffer);
                    
                    bmp.SetPixel(i, j, clrout);
                }
            }

            var bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGBA,
                bmp.Width, bmp.Height, 0, Gl.GL_RGBA,
                Gl.GL_UNSIGNED_BYTE, bmpData.Scan0);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);       // Linear Filtering
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

            return texID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RNG = new Random(Environment.TickCount);

            ScrW = 16;
            ScrH = 10;
            
            Glut.glutInit();
            // инициализация режима экрана 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA) 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();
            
            Gl.glOrtho(0.0, ScrW, 0.0, ScrH, 10, -10);
            
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glAlphaFunc(Gl.GL_GREATER, .0f);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthFunc(Gl.GL_LEQUAL);
            Gl.glClearDepth(1.0);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            
            player = new Player(34,65);
            
            EnemyList = new Dictionary<int, Enemy>();

            EnemyList.Add(0, new Ghost(38,32,RNG));
            EnemyList.Add(1, new Ghost(36,36,RNG));
            EnemyList.Add(2, new Ghost(38, 35, RNG));
            EnemyList.Add(3, new Ghost(36, 31, RNG));
            EnemyList.Add(4, new Ghost(38, 32, RNG));
            EnemyList.Add(5, new Ghost(36, 38, RNG));
            EnemyList.Add(6, new Ghost(38, 39, RNG));
            EnemyList.Add(7, new Ghost(36, 37, RNG));

            DropList = new List<Item>();

            Map MapReader = new Map("map.bmp");

            map = MapReader.MapArray();

            Map ObjMapReader = new Map("obj_map.bmp");

            obj_map = ObjMapReader.MapArray();



            player.Inventory.Add(new Potion(PotionType.Health, 2, 0, 0, false, DropList));
            player.Inventory.Add(new Potion(PotionType.Health, 4, 0, 0, false, DropList));
            player.Inventory.Add(new Potion(PotionType.Energy, 4, 0, 0, false, DropList));
            player.Inventory.Add(new Potion(PotionType.Speed, 4, 0, 0, false, DropList));
            player.Inventory.Add(new Potion(PotionType.Energy, 4, 0, 0, false, DropList));
            player.Inventory.Add(new Potion(PotionType.Health, 2, 0, 0, false, DropList));
            player.Inventory.Add(new Potion(PotionType.Health, 200, 0, 0, false, DropList));

            player.Inventory.Add(new Potion(PotionType.Health, 200, 0, 0, false, DropList));

            //objects
            //Загрузил 12 текстурок, вместо 3, ведь вращать не додумался 
            Textures.Add("RoadR", LoadTexture("Tex//RoadText//R.png"));
            Textures.Add("RoadD", LoadTexture("Tex//RoadText//D.png"));
            Textures.Add("RoadU", LoadTexture("Tex//RoadText//U.png"));
            Textures.Add("RoadL", LoadTexture("Tex//RoadText//L.png"));
            Textures.Add("RoadRU", LoadTexture("Tex//RoadText//RU.png"));
            Textures.Add("RoadRD", LoadTexture("Tex//RoadText//RD.png"));
            Textures.Add("RoadLD", LoadTexture("Tex//RoadText//LD.png"));
            Textures.Add("RoadLU", LoadTexture("Tex//RoadText//LU.png"));
            Textures.Add("RoadZUR", LoadTexture("Tex//RoadText//ZUR.png"));
            Textures.Add("RoadZDR", LoadTexture("Tex//RoadText//ZDR.png"));
            Textures.Add("RoadZDL", LoadTexture("Tex//RoadText//ZDL.png"));
            Textures.Add("RoadZUL", LoadTexture("Tex//RoadText//ZUL.png"));


            //textures
            Textures.Add("Ice", LoadTexture("Tex//Ice.png"));
            Textures.Add("Sand", LoadTexture("Tex//Sand.png"));
            Textures.Add("Ground", LoadTexture("Tex//Ground.png"));
            Textures.Add("Grass", LoadTexture("Tex//Grass.png"));
            Textures.Add("StWall", LoadTexture("Tex//StWall.bmp"));
            Textures.Add("StBlack", LoadTexture("Tex//StBlack.bmp"));
            Textures.Add("StFloor", LoadTexture("Tex//StFloor.bmp"));
            Textures.Add("WFloor", LoadTexture("Tex//WFloor.bmp"));

            //misc
            Textures.Add("Cursor", LoadTexture("Tex//сursor.png"));
            Textures.Add("HPBAR", LoadTexture("Tex//Hpbar.png"));
            Textures.Add("Menu", LoadTexture("Tex//MenuScreen.png"));
            Textures.Add("ItemBG", LoadTexture("Tex//ItemBG.png"));
            Textures.Add("DescBG", LoadTexture("Tex//DescBG.png"));

            //Items
            //Potions
            Textures.Add("ItemHP0", LoadTexture("Tex//Items//Potions//HP0.png"));
            Textures.Add("ItemSpeed0", LoadTexture("Tex//Items//Potions//Speed0.png"));
            Textures.Add("ItemEnergy0", LoadTexture("Tex//Items//Potions//Energy0.png"));


            //enemys
            Textures.Add("Ghost", LoadTexture("Tex//Ghost.png"));

            //idle
            Textures.Add("IdleD", LoadTexture("Tex//Idle//Down.png"));
            Textures.Add("IdleU", LoadTexture("Tex//Idle//Up.png"));
            Textures.Add("IdleL", LoadTexture("Tex//Idle//Left.png"));
            Textures.Add("IdleR", LoadTexture("Tex//Idle//Right.png"));
            //uprun
            Textures.Add("HeroUP0", LoadTexture("Tex//UpRun//0.png"));
            Textures.Add("HeroUP1", LoadTexture("Tex//UpRun//1.png"));
            Textures.Add("HeroUP2", LoadTexture("Tex//UpRun//2.png"));
            Textures.Add("HeroUP3", LoadTexture("Tex//UpRun//3.png"));
            Textures.Add("HeroUP4", LoadTexture("Tex//UpRun//4.png"));
            Textures.Add("HeroUP5", LoadTexture("Tex//UpRun//5.png"));
            Textures.Add("HeroUP6", LoadTexture("Tex//UpRun//6.png"));
            Textures.Add("HeroUP7", LoadTexture("Tex//UpRun//7.png"));
            //leftrun
            Textures.Add("HeroLEFT0", LoadTexture("Tex//LeftRun//0.png"));
            Textures.Add("HeroLEFT1", LoadTexture("Tex//LeftRun//1.png"));
            Textures.Add("HeroLEFT2", LoadTexture("Tex//LeftRun//2.png"));
            Textures.Add("HeroLEFT3", LoadTexture("Tex//LeftRun//3.png"));
            Textures.Add("HeroLEFT4", LoadTexture("Tex//LeftRun//4.png"));
            Textures.Add("HeroLEFT5", LoadTexture("Tex//LeftRun//5.png"));
            Textures.Add("HeroLEFT6", LoadTexture("Tex//LeftRun//6.png"));
            Textures.Add("HeroLEFT7", LoadTexture("Tex//LeftRun//7.png"));
            //downrun
            Textures.Add("HeroDOWN0", LoadTexture("Tex//DownRun//0.png"));
            Textures.Add("HeroDOWN1", LoadTexture("Tex//DownRun//1.png"));
            Textures.Add("HeroDOWN2", LoadTexture("Tex//DownRun//2.png"));
            Textures.Add("HeroDOWN3", LoadTexture("Tex//DownRun//3.png"));
            Textures.Add("HeroDOWN4", LoadTexture("Tex//DownRun//4.png"));
            Textures.Add("HeroDOWN5", LoadTexture("Tex//DownRun//5.png"));
            Textures.Add("HeroDOWN6", LoadTexture("Tex//DownRun//6.png"));
            Textures.Add("HeroDOWN7", LoadTexture("Tex//DownRun//7.png"));
            //downrun
            Textures.Add("HeroRIGHT0", LoadTexture("Tex//RightRun//0.png"));
            Textures.Add("HeroRIGHT1", LoadTexture("Tex//RightRun//1.png"));
            Textures.Add("HeroRIGHT2", LoadTexture("Tex//RightRun//2.png"));
            Textures.Add("HeroRIGHT3", LoadTexture("Tex//RightRun//3.png"));
            Textures.Add("HeroRIGHT4", LoadTexture("Tex//RightRun//4.png"));
            Textures.Add("HeroRIGHT5", LoadTexture("Tex//RightRun//5.png"));
            Textures.Add("HeroRIGHT6", LoadTexture("Tex//RightRun//6.png"));
            Textures.Add("HeroRIGHT7", LoadTexture("Tex//RightRun//7.png"));

            //attack
            Textures.Add("HeroAtkR0", LoadTexture("Tex//Attack//right_0.png"));
            Textures.Add("HeroAtkR1", LoadTexture("Tex//Attack//right_1.png"));
            Textures.Add("HeroAtkL0", LoadTexture("Tex//Attack//left_0.png"));
            Textures.Add("HeroAtkL1", LoadTexture("Tex//Attack//left_1.png"));
            Textures.Add("HeroAtkU0", LoadTexture("Tex//Attack//up_0.png"));
            Textures.Add("HeroAtkU1", LoadTexture("Tex//Attack//up_1.png"));
            Textures.Add("HeroAtkD0", LoadTexture("Tex//Attack//down_0.png"));
            Textures.Add("HeroAtkD1", LoadTexture("Tex//Attack//down_1.png"));


            // активация таймера, вызывающего функцию для визуализации 
            RenderTimer.Start();
            LogicTimer.Start();
        }

        private void ControlTimer_Tick(object sender, EventArgs e)
        {
            bool W = IsKeyDown(Keys.W);
            bool A = IsKeyDown(Keys.A);
            bool S = IsKeyDown(Keys.S);
            bool D = IsKeyDown(Keys.D);
            player.MovedByControl(W, A, S, D, map);
        }

        private void AnT_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) player.AttackFunc(GetNearbyEnemies(player.X, player.Y, EnemyList));
            if (e.Button == MouseButtons.Right) {
                int X = (int)((MouseOnMatrixX - 1.5f) * 4f / 3f);
                int Y = (int)((7f + 3f / 4f - MouseOnMatrixY) * 4f / 3f);
                if (X >= 0 && X < player.Inventory.Width && Y >= 0 && Y < player.Inventory.Height) player.Inventory.Activate((int)Y * player.Inventory.Width + (int)X);
            }
        }

        private void AnT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E) {
                Item Nearest = GetNearestItem();
                if (Nearest != null)
                {
                    Item pickup = (Item)Nearest.Clone();
                    DropList.Remove(Nearest);
                    player.Inventory.Add(pickup);
                }
            }
            if (InvMenu)
            {
                if (e.KeyCode == Keys.G)
                {
                    int X = (int)((MouseOnMatrixX - 1.5f) * 4f / 3f);
                    int Y = (int)((7f + 3f / 4f - MouseOnMatrixY) * 4f / 3f);
                    if (X >= 0 && X < player.Inventory.Width && Y >= 0 && Y < player.Inventory.Height) player.Inventory.Items[(int)Y * player.Inventory.Width + (int)X] = null;
                }

                if (e.KeyCode == Keys.Y)
                {
                    int X = (int)((MouseOnMatrixX - 1.5f) * 4f / 3f);
                    int Y = (int)((7f + 3f / 4f - MouseOnMatrixY) * 4f / 3f);
                    if (X >= 0 && X < player.Inventory.Width && Y >= 0 && Y < player.Inventory.Height && player.Inventory.Items[(int)Y * player.Inventory.Width + (int)X] != null) player.Inventory.Drop((int)Y * player.Inventory.Width + (int)X);
                }
                if (e.KeyCode == Keys.T)
                {
                    int X = (int)((MouseOnMatrixX - 1.5f) * 4f / 3f);
                    int Y = (int)((7f + 3f / 4f - MouseOnMatrixY) * 4f / 3f);
                    if (X >= 0 && X < player.Inventory.Width && Y >= 0 && Y < player.Inventory.Height && player.Inventory.Items[(int)Y * player.Inventory.Width + (int)X] != null) player.Inventory.DropOne((int)Y * player.Inventory.Width + (int)X);
                }
            }
            if (e.KeyCode == Keys.J) QuestMenu = !QuestMenu;
            if (e.KeyCode == Keys.I) InvMenu = !InvMenu;
            if (e.KeyCode == Keys.Escape) { InvMenu = false; QuestMenu = false; }
        }

        void MousePosOnAnt(out int X,out int Y, out float MouseOnMatrixX, out float MouseOnMatrixY) {
            X = Form1.MousePosition.X - this.Location.X - 8;
            Y = AnT.Height-(Form1.MousePosition.Y - this.Location.Y - 32);
            MouseOnMatrixX = (float)X / (float)AnT.Width * (float)ScrW;
            MouseOnMatrixY = (float)Y / (float)AnT.Height * (float)ScrH;
        }


        void Draw2DText(float X, float Y, float Z, float XW, float YW, int Text)
        {
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, Text);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(X, Y, Z);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(X, Y + YW, Z);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(X + XW, Y + YW, Z);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(X + XW, Y, Z);
            Gl.glEnd();
        }

        void Draw2DText(float X, float Y, float Z, float XW, float YW)
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(X, Y,Z);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(X, Y + YW,Z);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(X + XW, Y + YW,Z);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(X + XW, Y,Z);
            Gl.glEnd();
        }

        void Draw2DTextCent(float X, float Y, float Z, float XW, float YW, int Text)
        {
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, Text);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord3f(0, 1,Z); Gl.glVertex3f(X - XW / 2, Y - YW / 2,Z);
            Gl.glTexCoord3f(0, 0,Z); Gl.glVertex3f(X - XW / 2, Y + YW / 2,Z);
            Gl.glTexCoord3f(1, 0,Z); Gl.glVertex3f(X + XW / 2, Y + YW / 2,Z);
            Gl.glTexCoord3f(1, 1,Z); Gl.glVertex3f(X + XW / 2, Y - YW / 2,Z);
            Gl.glEnd();
        }

        void Draw2DTextCent(float X, float Y, float Z, float XW, float YW)
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord3f(0, 1, Z); Gl.glVertex3f(X - XW / 2, Y - YW / 2, Z);
            Gl.glTexCoord3f(0, 0, Z); Gl.glVertex3f(X - XW / 2, Y + YW / 2, Z);
            Gl.glTexCoord3f(1, 0, Z); Gl.glVertex3f(X + XW / 2, Y + YW / 2, Z);
            Gl.glTexCoord3f(1, 1, Z); Gl.glVertex3f(X + XW / 2, Y - YW / 2, Z);
            Gl.glEnd();
        }

        void DrawString(float X, float Y, float Z, IntPtr font, string text, float R, float G, float B, bool shadow) {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            
            if (shadow) {
                Gl.glColor3f(0, 0, 0);
                Gl.glRasterPos3f(X+.02f, Y+.02f, Z);
                Glut.glutBitmapString(font, text);
            }
            Gl.glColor3f(R, G, B);
            Gl.glRasterPos3f(X, Y, Z);
            Glut.glutBitmapString(font, text);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }

        void DrawStringCent(float X1,float X2, float Y, float Z, IntPtr font, string text, float R, float G, float B, bool shadow)
        {
            float X=X1+(X2-X1-Glut.glutBitmapLength(font,text)*ScrW/AnT.Width)/2;
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            if (shadow)
            {
                Gl.glColor3f(0, 0, 0);
                Gl.glRasterPos3f(X + .02f, Y + .02f, Z);
                Glut.glutBitmapString(font, text);
            }
            Gl.glColor3f(R, G, B);
            Gl.glRasterPos3f(X, Y, Z);
            Glut.glutBitmapString(font, text);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }


        Dictionary<int, Enemy> GetNearbyEnemies(float playerX, float playerY, Dictionary<int, Enemy> Enemies) {
            Dictionary<int, Enemy> output = new Dictionary<int, Enemy>();
            foreach (KeyValuePair<int, Enemy> enemy in Enemies) {
                if (Math.Sqrt((playerX - enemy.Value.X) * (playerX - enemy.Value.X) + (playerY - enemy.Value.Y) * (playerY - enemy.Value.Y)) < 11f) {
                    output.Add(enemy.Key,enemy.Value);
                }
            }
            return output;
        }

        List<Item> GetNearbyItems(float playerX, float playerY,  List<Item> Items)
        {
            List<Item> output = new List<Item>();
            foreach (Item enemy in Items)
            {
                if (Math.Sqrt((playerX - enemy.X) * (playerX - enemy.X) + (playerY - enemy.Y) * (playerY - enemy.Y)) < 11f)
                {
                    output.Add(enemy);
                }
            }
            return output;
        }

        Item GetNearestItem() {
            if (DropList.Count == 0) return null;
            Item Nearest = DropList.First();
            double ThisDist;
            double NearestDist = Math.Sqrt((Nearest.X - player.X) * (Nearest.X - player.X) + (Nearest.Y - player.Y) * (Nearest.Y - player.Y));
            foreach (Item drop in DropList)
            {
                ThisDist = Math.Sqrt((drop.X - player.X) * (drop.X - player.X) + (drop.Y - player.Y) * (drop.Y - player.Y));
                if (ThisDist < NearestDist)
                {
                    NearestDist = ThisDist;
                    Nearest = drop;
                }
            }
            if (NearestDist < 1.5f) return Nearest;
            else return null;
            
        }

        public static bool IsKeyDown(Keys key)
        {
            return (GetKeyState(Convert.ToInt16(key)) & 0X80) == 0X80;
        }

        private void AnT_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void AnT_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }
        
        [DllImport("user32.dll")]
        public extern static Int16 GetKeyState(Int16 nVirtKey);
    }
}