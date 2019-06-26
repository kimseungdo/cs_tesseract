using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MetroFramework.Forms;

using Tesseract;
using Emgu.CV.Structure;
using Emgu.CV;

namespace end_cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {//다이얼로그 생성 이미지 삽입
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(dialog.FileName);
            }
        }

        Image img;
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {//Occurs when a drag-and - drop operation is completed.
         //끌어놓기 작업이 완료될때 발생
            foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                img = Image.FromFile(pic);
                pictureBox1.Image = img;
            }
        }//end DragDrop

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {//Occurs when an object is dragged into the control's bounds.
         //마우스로 항목을 끌어 이컨트롤 클라이언트 영역으로 가져올떄 발생
            e.Effect = DragDropEffects.Copy;
        }//end DragEnter

        private void button2_Click(object sender, EventArgs e)
        {//저장버튼
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.txt |All files (*.*)|(*.*)";
            saveFileDialog1.Title = "Save an Text File";
            saveFileDialog1.ShowDialog();

            if(saveFileDialog1.ShowDialog() == DialogResult.OK){
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n"
                        + textBox1.Text);
            }
            
        }//end 저장버튼클릭
        private void button3_Click_1(object sender, EventArgs e)
        {//변환버튼
            textBox1.ResetText();

            if (kor_button.Checked)
            {
                if(pictureBox1.Image == null)
                {
                    MessageBox.Show("이미지를 넣으세요~");
                }//end picturebox null
                else
                {
                    Bitmap img = new Bitmap(pictureBox1.Image);
                    var ocr = new TesseractEngine("./tessdata", "kor", EngineMode.Default);
                    var texts = ocr.Process(img);
                    //MessageBox.Show(texts.GetText());
                    textBox1.AppendText(texts.GetText());
                }
                
            }//end kor check
            else if(eng_button.Checked)
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("이미지를 넣으세요~");
                }
                else
                {
                    Bitmap img = new Bitmap(pictureBox1.Image);
                    var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.TesseractAndCube);
                    var texts = ocr.Process(img);
                    //MessageBox.Show(texts.GetText());
                    textBox1.AppendText(texts.GetText());
                }
            }//end eng check
            else
            {
                MessageBox.Show("언어를 선택하세요~");
            }
        }//end 변환버튼클릭
        
    }
}