﻿namespace DX
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.ControlTimer = new System.Windows.Forms.Timer(this.components);
            this.LogicTimer = new System.Windows.Forms.Timer(this.components);
            this.QuestCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.LoginScreenRenderTimer = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 5;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(-1, 0);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(1024, 640);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 0;
            this.AnT.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AnT_KeyUp);
            this.AnT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseClick);
            this.AnT.MouseEnter += new System.EventHandler(this.AnT_MouseEnter);
            this.AnT.MouseLeave += new System.EventHandler(this.AnT_MouseLeave);
            // 
            // ControlTimer
            // 
            this.ControlTimer.Enabled = true;
            this.ControlTimer.Interval = 5;
            this.ControlTimer.Tick += new System.EventHandler(this.ControlTimer_Tick);
            // 
            // LogicTimer
            // 
            this.LogicTimer.Interval = 5;
            this.LogicTimer.Tick += new System.EventHandler(this.LogicTimer_Tick);
            // 
            // QuestCheckTimer
            // 
            this.QuestCheckTimer.Tick += new System.EventHandler(this.QuestCheckTimer_Tick);
            // 
            // LoginScreenRenderTimer
            // 
            this.LoginScreenRenderTimer.Interval = 5;
            this.LoginScreenRenderTimer.Tick += new System.EventHandler(this.LoginScreenRenderTimer_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(476, 309);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(488, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Войти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 640);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.AnT);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer RenderTimer;
        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Timer ControlTimer;
        private System.Windows.Forms.Timer LogicTimer;
        private System.Windows.Forms.Timer QuestCheckTimer;
        private System.Windows.Forms.Timer LoginScreenRenderTimer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

