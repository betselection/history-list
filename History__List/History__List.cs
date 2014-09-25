//  History__List.cs
//
//  Author:
//       Victor L. Senior (VLS) <betselection(&)gmail.com>
//
//  Web: 
//       http://betselection.cc/betsoftware/
//
//  Sources:
//       http://github.com/betselection/
//
//  Copyright (c) 2014 Victor L. Senior
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

/// <summary>
/// History list module.
/// </summary>
namespace History__List
{
    // Directives
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// History list class.
    /// </summary>
    public class History__List : Form
    {
        /// <summary>
        /// The marshal object.
        /// </summary>
        private object marshal = null;

        /// <summary>
        /// The history list box.
        /// </summary>
        private ListBox historyListBox;

        /// <summary>
        /// The list box context menu strip.
        /// </summary>
        private ContextMenuStrip listBoxContextMenuStrip;

        /// <summary>
        /// The save tool strip menu item.
        /// </summary>
        private ToolStripMenuItem saveToolStripMenuItem;

        /// <summary>
        /// The copy tool strip menu item.
        /// </summary>
        private ToolStripMenuItem copyToolStripMenuItem;

        /// <summary>
        /// The roulette instance.
        /// </summary>
        private Roulette roulette = new Roulette();

        /// <summary>
        /// Initializes a new instance of the <see cref="History__List.History__List"/> class.
        /// </summary>
        public History__List()
        {
            this.historyListBox = new ListBox();
            this.listBoxContextMenuStrip = new ContextMenuStrip();
            this.copyToolStripMenuItem = new ToolStripMenuItem();
            this.saveToolStripMenuItem = new ToolStripMenuItem();
            this.SuspendLayout();

            // lbHistory
            this.historyListBox.Dock = DockStyle.Fill;
            this.historyListBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.historyListBox.DrawItem += new DrawItemEventHandler(this.HistoryListBox_DrawItem);
            this.historyListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.historyListBox.FormattingEnabled = true;
            this.historyListBox.ItemHeight = 27;
            this.historyListBox.Location = new System.Drawing.Point(0, 0);
            this.historyListBox.Name = "lbHistory";
            this.historyListBox.SelectionMode = SelectionMode.None;
            this.historyListBox.Size = new System.Drawing.Size(50, 366);
            this.historyListBox.TabIndex = 155;
            this.historyListBox.ContextMenuStrip = this.listBoxContextMenuStrip;

            // listBoxContextMenuStrip
            this.listBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    this.copyToolStripMenuItem,
                    this.saveToolStripMenuItem
                });
            this.listBoxContextMenuStrip.Name = "listBoxContextMenuStrip";
            this.listBoxContextMenuStrip.Size = new System.Drawing.Size(153, 70);

            // copyToolStripMenuItem
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            this.copyToolStripMenuItem.Image = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("files.png"));

            // saveToolStripMenuItem
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            this.saveToolStripMenuItem.Image = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("save.png"));

            // Module form
            this.ClientSize = new System.Drawing.Size(60, 366);
            this.Controls.Add(this.historyListBox);
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.Name = "HistoryList";
            this.Text = "History";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Inits the instance.
        /// </summary>
        /// <param name="passedMarshal">Passed marshal.</param>
        public void Init(object passedMarshal)
        {
            // Set marshal
            this.marshal = passedMarshal;

            // Set icon
            this.Icon = (Icon)this.marshal.GetType().GetProperty("Icon").GetValue(this.marshal, null);

            // Show form
            this.Show();
        }

        /// <summary>
        /// Processes input.
        /// </summary>
        public void Input()
        {
            // Set last
            string last = (string)this.marshal.GetType().GetProperty("Last").GetValue(this.marshal, null);

            // Check if undo
            if (last == "-U")
            {
                // Remove
                if (this.historyListBox.Items.Count > 0)
                {
                    this.historyListBox.Items.RemoveAt(0);
                }
            }
            else
            {
                // Add number
                this.historyListBox.Items.Insert(0, last);
            }

            // Update title
            this.UpdateCount();
        }

        /// <summary>
        /// Draws items in history listbox
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void HistoryListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Set sender's ItemHeight
                ((ListBox)sender).ItemHeight = e.Font.Height + 3;

                // Drawing routines
                e.DrawBackground();

                // Set brush
                SolidBrush myBrush = new SolidBrush(roulette.GetColor(Convert.ToByte(((ListBox)sender).Items[e.Index])));

                // Set number string
                string number = ((ListBox)sender).Items[e.Index].ToString();

                // Resolve 00
                if (number == "37")
                {
                    // Change into 00
                    number = "00";
                }

                // Draw roulette number
                e.Graphics.DrawString(number, e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                // No action
            }
        }

        /// <summary>
        /// Updates the count.
        /// </summary>
        private void UpdateCount()
        {
            // Check count
            if (this.historyListBox.Items.Count > 0)
            {
                // Change title
                this.Text = "H.(" + this.historyListBox.Items.Count.ToString() + ")";
            }
            else
            {
                // Set initial title
                this.Text = "History";
            }
        }

        /// <summary>
        /// Copies the history to clipboard
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void CopyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Check there's something
            if (this.historyListBox.Items.Count == 0)
            {
                // Exit 
                return;
            }

            // Clear clipboard
            Clipboard.Clear();

            // History string
            string historyList = string.Empty;

            // Add items in reversed order
            for (int h = this.historyListBox.Items.Count - 1; h > -1; h--)
            {
                // Add current item
                historyList += this.historyListBox.Items[h] + Environment.NewLine;
            }

            // Trim last newline element
            historyList.TrimEnd();

            // Copy to clipboard
            Clipboard.SetText(historyList);

            // Advice user
            MessageBox.Show("History was copied to clipboard.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Saves the history to file
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void SaveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Check there's something
            if (this.historyListBox.Items.Count == 0)
            {
                // Exit 
                return;
            }

            // Declare save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set filter
            saveFileDialog.Filter = "Text File | *.txt";

            // TODO try-catch to handle exceptions

            // Check a valid file was entered
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            { 
                // Write via StreamWriter
                using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                { 
                    // Add items in reversed order
                    for (int h = this.historyListBox.Items.Count - 1; h > -1; h--)
                    { 
                        // Write current line
                        writer.WriteLine(this.historyListBox.Items[h]);
                    } 
                } 
            }

            // Advice user
            MessageBox.Show("History was saved to file:" + Environment.NewLine + saveFileDialog.FileName, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}