// Output_GUI.cs created with MonoDevelop at 6:00 PM 1/29/2009
// @author mbirth

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace vampi {
    
    public class Output_GUI : Output {
        const int statsHeight = 45;
        int scale = 1;
        Form f;
        FastPixel fp;
        Graphics fg;
        Bitmap field;
        Bitmap stats;
        
        public Output_GUI() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            this.f = new Form();
            this.field = new Bitmap(Settings.size, Settings.size, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            this.stats = new Bitmap(f.ClientSize.Width, Output_GUI.statsHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            f.Text = "Vampi GUI --- ©2008 Markus Birth, FA76";
            
            // attach some events
            f.Closing += new System.ComponentModel.CancelEventHandler(FormClosing);  // fires when "X" clicked
            f.Resize += new EventHandler(FormResize);  // fires while resizing
            f.ResizeEnd += new EventHandler(FormResize);   // fires after finish resizing or moving window

            // clear gameField with emptyColor
            Graphics g = Graphics.FromImage(this.field);
            g.Clear(Settings.guiColorEmpty);
            g.Dispose();
            
            if (Math.Min(f.ClientSize.Width, f.ClientSize.Height-Output_GUI.statsHeight) < Settings.size) {
                f.ClientSize = new Size(Settings.size, Settings.size+Output_GUI.statsHeight);
            }

            fp = new FastPixel(field);
            fg = Graphics.FromHwnd(f.Handle);
            fg.InterpolationMode = InterpolationMode.NearestNeighbor;
            fg.Clear(Color.White);   // clear window background
            f.Show();
            calculateScale();
        }
        
        ~Output_GUI() {
            fg.Dispose();
            f.Dispose();
        }
        
        public void FormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            requestAbort = true;
            f.Hide();
        }
        
        public void calculateScale() {
            scale = (int)Math.Floor((double)Math.Min(f.ClientSize.Height - Output_GUI.statsHeight, f.ClientSize.Width) / (double)Settings.size);
            if (scale<1) scale = 1;
        }
        
        public void FormResize(object sender, EventArgs e) {
            calculateScale();
            stats = new Bitmap(f.ClientSize.Width, Output_GUI.statsHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            fg = Graphics.FromHwnd(f.Handle);
            fg.InterpolationMode = InterpolationMode.NearestNeighbor;
            fg.Clear(Color.White);
        }
        
        public override void doOutput() {
            this.drawGameMap();
            this.drawStatistics();
            Application.DoEvents();
        }
        
        protected Color getGradient(Color[] gradientMap, double percent) {
            int mapSteps = gradientMap.GetLength(0)-1;
            double percSteps = 100/(double)mapSteps;
            int bestMatch = (int)(percent/percSteps);
            if (percent/percSteps == Math.Floor(percent/percSteps)) return gradientMap[bestMatch];  // shortcut
            int curStep = (int)Math.Floor(percent/percSteps);
            if (curStep == gradientMap.GetLength(0)-1) curStep--;
            Color left = gradientMap[curStep];
            double leftPerc = curStep*percSteps;
            Color right = gradientMap[curStep+1];
            double rightPerc = (curStep+1)*percSteps;
            
            percent = (percent-leftPerc) * (100/(rightPerc-leftPerc));
            
            Color final = Color.FromArgb(getPercentage(left.R, right.R, percent), getPercentage(left.G, right.G, percent), getPercentage(left.B, right.B, percent));
            
            return final;
        }
        
        protected Color getGradient(Color fixedColor, double percent) {
            return fixedColor;
        }
        
        protected byte getPercentage(byte col1, byte col2, double percent) {
            return (byte)(col1 + (col2-col1)*percent/100);
        }
        
        public void drawGameMap() {
            fp.Lock();
            Position pos;
            Color c = Color.Black;
            for (pos.y = 1; pos.y <= Settings.size; pos.y++) {
                for (pos.x = 1; pos.x <= Settings.size; pos.x++) {
                    Spielfigur sf = Program.sflaeche.getSpielfeld(pos).Sfigur;

                    if (sf == null) c = Settings.guiColorEmpty;   //Spielfeld leer
                    else { //Spielfeld besetzt
                        switch (sf.Typ) {
                            case Spielfigur.TYPE_HUMAN:
                                if (((Einwohner)sf).Infected)
                                    c = getGradient(Settings.guiColorHumanInfected, 100-(double)(sf.Age-Settings.humanMaxAge+Settings.humanInfectedMaxAge)*100/(double)Settings.humanInfectedMaxAge);
                                else
                                    if (sf.props[Einwohner.F_SEX] == Einwohner.SEX_MALE)
                                        c = getGradient(Settings.guiColorHumanMale, 100-(double)sf.Age*100/(double)Settings.humanMaxAge);
                                    else
                                        c = getGradient(Settings.guiColorHumanFemale, 100-(double)sf.Age*100/(double)Settings.humanMaxAge);
                                break;
                            case Spielfigur.TYPE_VAMPIRE:
                                c = getGradient(Settings.guiColorVampire, 100-(double)sf.Age*100/(double)Settings.vampireMaxAge);
                                break;
                        }
                    }
                    fp.SetPixel(pos.x-1, pos.y-1, c);
                }
            }
            fp.Unlock(true);
            fg.DrawImage(this.field, 0, Output_GUI.statsHeight, Settings.size * this.scale, Settings.size * this.scale);
            f.Update();
        }
        
        public void drawStatistics() {
            if (!f.Visible) this.requestAbort = true;
            Graphics g = Graphics.FromImage(this.stats);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.Clear(Color.White);
            int lineSpc = 10;
            
            int Ecount = Einwohner.Count; // sflaeche.countTypeOccurrences(Typliste.EINWOHNER);
            int Vcount = Vampir.Count;    // sflaeche.countTypeOccurrences(Typliste.VAMPIR);
            g.DrawString(String.Format("Step: {0:D5}", Program.AnzSimDone), Settings.guiFont, Settings.guiFontBrush, 5, 0);
            g.DrawString(String.Format("T{0:N} = {1:D}/sec", Program.lastOverallTime, (int)Math.Floor(1000/(Program.lastOverallTime))), Settings.guiFont, Settings.guiFontBrush, 100, 0);
            g.DrawString(String.Format("C{0:N} D{1:N}", Program.lastCalcTime, Program.lastStatsTime), Settings.guiFont, Settings.guiFontBrush, 200, 0);
            g.DrawString(String.Format(String.Format("Humans: {0:D} / Vampires: {1:D}", Ecount, Vcount), Program.AnzSimDone), Settings.guiFont, Settings.guiFontBrush, 5, 1*lineSpc);
            g.DrawString(String.Format(String.Format("V/H Ratio = 1/{0:N5}", (double)Ecount / Vcount), Program.AnzSimDone), Settings.guiFont, Settings.guiFontBrush, 5, 2*lineSpc);
            g.DrawString(String.Format(String.Format("Coverage: {0:N5} %", (double)(Ecount + Vcount) / (Settings.size*Settings.size) * 100), Program.AnzSimDone), Settings.guiFont, Settings.guiFontBrush, 5, 3*lineSpc);
            g.Dispose();
            g = Graphics.FromHwnd(f.Handle);
            g.DrawImageUnscaled(this.stats, 0, 0);
            g.Dispose();
        }
    }
}
