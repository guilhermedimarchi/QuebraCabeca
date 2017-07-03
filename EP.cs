using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace EP
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class EP : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button b00;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Button [,] BL;
		private System.Windows.Forms.Button btnSolve;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button bRand;
		private int [] table;


		public EP()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//table = new int [] { 0,1,2,3,4,5,6,7,8 };
            table = new int [] { 0, 1,2,3,4,5,6,7,8 };
            //table = new int [] { 3,6,4,2,1,0,5,7,8 };

			// Draws the painel
			DrawPanel();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.b00 = new System.Windows.Forms.Button();
			this.btnSolve = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.bRand = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// b00
			// 
			this.b00.BackColor = System.Drawing.Color.White;
			this.b00.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b00.Location = new System.Drawing.Point(8, 8);
			this.b00.Name = "b00";
			this.b00.Size = new System.Drawing.Size(56, 56);
			this.b00.TabIndex = 2;
			this.b00.Text = "X";
			this.b00.Visible = false;
			this.b00.Click += new System.EventHandler(this.b00_Click);
			// 
			// btnSolve
			// 
			this.btnSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSolve.Location = new System.Drawing.Point(8, 192);
			this.btnSolve.Name = "btnSolve";
			this.btnSolve.Size = new System.Drawing.Size(176, 40);
			this.btnSolve.TabIndex = 3;
			this.btnSolve.Text = "Solução";
			this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
			// 
			// txtResult
			// 
			this.txtResult.Location = new System.Drawing.Point(8, 240);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtResult.Size = new System.Drawing.Size(176, 80);
			this.txtResult.TabIndex = 4;
			this.txtResult.Text = "";
			// 
			// bRand
			// 
			this.bRand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bRand.Location = new System.Drawing.Point(8, 328);
			this.bRand.Name = "bRand";
			this.bRand.Size = new System.Drawing.Size(176, 40);
			this.bRand.TabIndex = 5;
			this.bRand.Text = "Sortear";
			this.bRand.Click += new System.EventHandler(this.bRand_Click);
			// 
			// EP
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(192, 374);
			this.Controls.Add(this.bRand);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnSolve);
			this.Controls.Add(this.b00);
			this.Name = "EP";
			this.Text = "Quebra Cabeça";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new EP());
		}

		/// <summary>
		/// Draws the game panel
		/// </summary>
		private void DrawPanel()
		{
			// Creating the labels
			BL = new Button[3,3];
			for (int i=0; i<3; i++)
			{
				for (int j=0; j<3; j++)
				{
					Button B = new Button();
					B.Parent = b00.Parent;
					B.Font = b00.Font;
					B.Size = b00.Size;
					B.Left = i*56 + 10;
					B.Top = j*56 + 10;
					B.BackColor = b00.BackColor;
					B.Text = (table[j*3+i]!=0?table[j*3+i].ToString():"");
					B.Visible = true;
					B.Click += new System.EventHandler(this.b00_Click);
					BL[i,j] = B;
				}
			}

		}
		

		/// <summary>
		/// Click template for all buttons 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void b00_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			int x1=0, y1=0, x2=0, y2=0;
			for (int i=0; i<3; i++)
			{
				for (int j=0; j<3; j++)
				{
					// search for button
					if (BL[i,j] == b)
					{
						// Move to clear position 
						x2 = x1 = i;
						y2 = y1 = j;
						if ((i>0) && (BL[i-1,j].Text==""))
							x2--;
						if ((i<2) && (BL[i+1,j].Text==""))
							x2++;
						if ((j>0) && (BL[i,j-1].Text==""))
							y2--;
						if ((j<2) && (BL[i,j+1].Text==""))
							y2++;
					}
				}
			}
			// Chances clear with clicked
			string temp = BL[x1,y1].Text;
			BL[x1,y1].Text= BL[x2,y2].Text;
			BL[x2,y2].Text = temp;
		}


		/// <summary>
		/// Solves the puzzle
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSolve_Click(object sender, System.EventArgs e)
		{
			// Clears memo
			txtResult.Text = "";
			
			// First, update table structure with buttons configuration
			int k = 0;
			for (int i=0; i<3; i++)
				for (int j=0; j<3; j++)
					table[k++] = (BL[j,i].Text==""?0:Convert.ToInt32(BL[j,i].Text));
			
			// Now the agent looks for the parameter target
			EightPuzzle ComputerAgent = new EightPuzzle(table, new int [] {0,1,2,3,4,5,6,7,8});
			
			// Writes the solution to the tet box
			int [] res = ComputerAgent.GetSolution();
			if (res == null)
				txtResult.Text += "Tamanho limite de busca alcançado.";
			else
			{
				foreach(int i in res)
					txtResult.Text += i.ToString() + ",";
				txtResult.Text += "#";
				txtResult.Text = txtResult.Text.Replace(",#","");
			}
		}

		/// <summary>
		/// Random configure the table
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bRand_Click(object sender, System.EventArgs e)
		{
			Random r = new Random(DateTime.Now.Millisecond);		
			for (int i=0;i<100;i++)
			{
				int p1 = r.Next(9);
				int p2 = r.Next(9);
				int temp = table[p1];
				table[p1] = table[p2];
				table[p2] = temp;
			}
			int k = 0;
			for (int i=0; i<3; i++)
				for (int j=0; j<3; j++)
				{
					BL[j,i].Text = (table[k]==0?"":table[k].ToString());
					k++;
				}

		}

	}
}
