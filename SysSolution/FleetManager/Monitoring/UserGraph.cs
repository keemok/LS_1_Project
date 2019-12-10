using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Drawing;

namespace SysSolution.FleetManager.Monitoring
{
    public partial class UserGraph : UserControl
    {
        /// <summary>
        /// 격자(Grid)의 간격, 막대그래프의 너비
        /// </summary>
        private int interval = 0;



        /// <summary>
        /// 전달받은 값들을 저장할 List객체(막대그래프)
        /// </summary>
        public List<int> graphValues = new List<int>();



        /// <summary>
        /// 전달받은 값들을 저장할 List객체(꺾은선그래프)
        /// </summary>

        public List<Point> graphLines = new List<Point>();



        /// <summary>
        /// 격자간격으로 그래프를 초기화 합니다.
        /// </summary>
        /// <param name="interval">지정할 격자간격</param>
        public UserGraph(int interval) : this()

        {

            this.Size = new Size(401, 401);

            this.interval = interval;// 전달받은 인수를 적용

            this.Paint += delegate (object s, PaintEventArgs e)

            {

                this.DrawGrid();// 격자 그리기

            };

        }

        public UserGraph()
        {
            InitializeComponent();
        }

        private void UserGraph_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 꺾은선 그래프를 그려주는 메서드
        /// </summary>
        public void DrawLine()
        {
            for (int idx = 0; idx < this.graphLines.Count; idx++)
            {
                if (idx == 0) continue;// 전달받은값이 1개밖에 없다면 그리지 않음

                Graphics g = this.CreateGraphics();
                Pen linePen = new Pen(Color.Red, 2);
                g.DrawLine(linePen, this.graphLines[idx - 1], this.graphLines[idx]);
            }
        }

        /// <summary>
        /// 막대그래프를 그려주는 메서드
        /// </summary>
        /// <param name="value">이 항목이 표시해야할 값</param>
        public void DrawBar(int value)
        {
            this.graphValues.Add(value);// 전달받은 값을 해당List객체에 저장

            this.Paint += delegate (object s, PaintEventArgs e)
            {
                for (int idx = 0; idx < this.graphValues.Count; idx++)
                {// 전달받은 값들을 그려줌
                    // 막대의 높이
                    int barHeight = this.Height * this.graphValues[idx] / 100;

                    // 단일 막대를 의미하는 Rectangle 객체
                    Rectangle rec = new Rectangle(idx * this.interval, this.Height - barHeight - 1, this.interval, barHeight);

                    // 막대그래프의 외곽선을 그릴 Pen객체
                    Pen barPen = new Pen(Color.White, 2.5f);

                    e.Graphics.DrawRectangle(barPen, rec);// 외곽선 그리기
                    e.Graphics.FillRectangle(Brushes.Blue, rec);// 내부 채우기

                    // 꺾은선 그래프를 그릴 Point좌표 계산
                    Point linePoint = rec.Location;
                    linePoint.Offset(this.interval / 2, 0);

                    // 중복되지 않을때만 꺾은선그래프 List에 저장
                    if (!this.graphLines.Contains(linePoint)) this.graphLines.Add(linePoint);
                }
                this.DrawLine();// 꺾은선 그래프 그리기
            };
            this.Refresh();// Paint이벤트 호출
        }

        /// <summary>
        /// 격자선을 그려주는 메서드
        /// </summary>
        private void DrawGrid()
        {
            // 간격지정..
            Graphics g = this.CreateGraphics();
            Pen gridPen = new Pen(Color.Gray, 0.5f);

            // 가로선
            for (int i = 0; i * interval < this.ClientRectangle.Height; i++)
            {
                g.DrawLine(gridPen, new Point(0, i * this.interval), new Point(this.Width, i * this.interval));
            }
            // 세로선
            for (int i = 0; i * interval < this.ClientRectangle.Width; i++)
            {
                g.DrawLine(gridPen, new Point(i * this.interval, 0), new Point(i * this.interval, this.ClientRectangle.Height));
            }// end of for
        }// end of method
    }
}
