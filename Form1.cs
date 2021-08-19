using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomColorSelector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.htmlEditControl1.ToolStripItems.Find("tsbForeColor", false)[0].Visible = false;
            this.htmlEditControl1.ToolStripItems.Find("tsbBackColor", false)[0].Visible = false;
            this.htmlEditControl1.ToolStripItems.Find("ToolStripSeparator7", false)[0].Visible = false;

            ToolStripDropDownButton oForeColor = new ToolStripDropDownButton("ForeColor")
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                Padding = new Padding(3),
                Name = "ForeColor"
            };

            oForeColor.DropDownItems.Add("Green");
            oForeColor.DropDownItems.Add("Blue");
            oForeColor.DropDownItems.Add("Red");
            oForeColor.DropDownItems.Add("Yellow");
            oForeColor.DropDownItems.Add("Black");
            oForeColor.DropDownItems.Add("White");
            oForeColor.DropDownItems.Add("#766512");
            oForeColor.DropDownItems.Add("#2312AA");

            foreach (dynamic oItem in oForeColor.DropDownItems)
            {
                if (Color.FromName(oItem.Text).IsKnownColor)
                    oItem.Image = GetImage(Color.FromName(oItem.Text));
                else
                    oItem.Image = GetImage(ColorTranslator.FromHtml(oItem.Text));
            }

            oForeColor.DropDownItemClicked += OForeColor_DropDownItemClicked;
            this.htmlEditControl1.ToolStripItems.Add(oForeColor);

            ToolStripDropDownButton oBackColor = new ToolStripDropDownButton("BackColor")
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                Padding = new Padding(3),
                Name = "BackColor"
            };

            oBackColor.DropDownItems.Add("Green");
            oBackColor.DropDownItems.Add("Blue");
            oBackColor.DropDownItems.Add("Red");
            oBackColor.DropDownItems.Add("Yellow");
            oBackColor.DropDownItems.Add("Black");
            oBackColor.DropDownItems.Add("White");
            oBackColor.DropDownItems.Add("#766512");
            oBackColor.DropDownItems.Add("#2312AA");

            foreach (dynamic oItem in oBackColor.DropDownItems)
            {
                if (Color.FromName(oItem.Text).IsKnownColor)
                    oItem.Image = GetImage(Color.FromName(oItem.Text));
                else
                    oItem.Image = GetImage(ColorTranslator.FromHtml(oItem.Text));
            }

            oBackColor.DropDownItemClicked += OBackColor_DropDownItemClicked;
            this.htmlEditControl1.ToolStripItems.Add(oBackColor);

            this.htmlEditControl1.UserInteraction += HtmlEditControl1_UserInteraction;
        }

        private void HtmlEditControl1_UserInteraction(object sender, Zoople.UserInteractionEventsArgs e)
        {
            this.htmlEditControl1.ToolStripItems.Find("ForeColor", false)[0].Image = GetImage(this.htmlEditControl1.CurrentSelection.ForeColorARGB);
            this.htmlEditControl1.ToolStripItems.Find("BackColor", false)[0].Image = GetImage(this.htmlEditControl1.CurrentSelection.BackColorARBG);
        }

        private void OBackColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.htmlEditControl1.Document.ExecCommand("BackColor", false, e.ClickedItem.Text);
        }

        private void OForeColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.htmlEditControl1.Document.ExecCommand("ForeColor", false, e.ClickedItem.Text);
        }

        private Image GetImage(Color color)
        {
            Bitmap Bmp = new Bitmap(32, 32);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(color))
            {
                gfx.FillRectangle(brush, 0, 0, 32, 32);
            }
            return Bmp;
        }
    }
}
