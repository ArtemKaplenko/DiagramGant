using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGanta
{
    class Cage
    {
        public Rectangle rect;
        public int widthCage;
        public int heightCage;

        public bool selected;
        //public bool fortask;
        public bool isTask;

        public TaskModel task;

        public Cage()
        {
            widthCage = 120;
            heightCage = 20;
            selected = false;
            isTask = false;
        }

        public void ToTask(Graphics g, string nameTask)
        {
            task = new TaskModel();
            task.taskName = nameTask;
            //SizeF s = g.MeasureString(task.taskName, font);
            g.DrawString(task.taskName, new Font("Arial", 10), Brushes.Black, new Point(rect.X, rect.Y));
        }

        public void DrawTask(Graphics g)
        {
            g.FillRectangle(Brushes.White, rect);
            g.DrawString(task.taskName, new Font("Arial", 10), Brushes.Black, new Point(rect.X, rect.Y));
        }
    }

    class CageTime
    {
        public Rectangle rect;
        public int timeWidth;
        public Brush color;
        public bool selected;
        public float timeTask;

        public CageTime(int x, int y, int time, Brush br)
        {
            timeWidth = 120 * time;
            color = br;
            rect = new Rectangle(x, y, timeWidth, 20);
            selected = false;
            timeTask = time;
        }

        public void reTime()
        {
            if (rect.Width % 120 == 0)
                timeTask = rect.Width / 120;
            else
                timeTask = rect.Width / 2;
        }

        public bool inBorder(Point p)
        {
            if (p.X > rect.X+ rect.Width - 5 && p.X < rect.X+rect.Width + 5 && p.Y > rect.Y && p.Y < rect.Y+rect.Height)
                return true;
            else
                return false;
        }
    }

    class Diagram
    {
        public List<Cage> field;
        public Cage cage;
        public int rowsCount;
        public int columnCount;
        public List<CageTime> cageTimes;
        CageTime cageT;
        public float timeOfProject;

        public Diagram()
        {
            rowsCount =25;
            columnCount = 20;

            cageTimes = new List<CageTime>();

            field = new List<Cage>();
            for(int i = 0; i<rowsCount; i++)
            {
                for(int j = 0; j<columnCount; j++)
                {
                    cage = new Cage();
                    cage.rect = new Rectangle(j * cage.widthCage, i * cage.heightCage + cage.heightCage, cage.widthCage, cage.heightCage);
                    field.Add(cage);
                }
            }
        }

        public void DrawDiagram(Graphics g)
        {
            for(int i=1; i<columnCount; i++)
            {
                g.DrawString($"{i} час", new Font("Arial", 10), Brushes.Black, new Point(field[1].rect.X + field[1].widthCage*i-10, 0));
            }
            foreach(CageTime cageT in cageTimes)
            {
                g.FillRectangle(cageT.color, cageT.rect);
                if(cageT.rect.Width % 120 == 0)
                    g.DrawString($"{cageT.timeTask+" час"}",new Font("Arial", 10), Brushes.Black, new Point(cageT.rect.X + cageT.rect.Width+ 5, cageT.rect.Y + 2));
                else
                    g.DrawString($"{cageT.timeTask+" мин"}", new Font("Arial", 10), Brushes.Black, new Point(cageT.rect.X + cageT.rect.Width + 5, cageT.rect.Y + 2));
            }
            for (int i = 0; i < field.Count; i++)
            {
                g.DrawRectangle(Pens.Black, field[i].rect);
            }
            for (int i = 0; i < field.Count; i++)
            {
                if (field[i].isTask)
                {
                    field[i].DrawTask(g);
                    g.DrawRectangle(Pens.Black, field[i].rect);
                }
                else if (!field[i].isTask && i % 20 == 0)
                {
                    g.DrawString("+", new Font("Arial", 10), Brushes.Black, new Point(field[i].rect.Width / 2 - 7, field[i].rect.Y));
                }
            }

            foreach (Cage cage in field)
            {
                if (cage.selected)
                    g.DrawRectangle(Pens.Blue, cage.rect);
            }
        }

        public void Select(MouseEventArgs e)
        {
            foreach (Cage cage in field)
            {
                if (cage.rect.Top < e.Y && cage.rect.Bottom > e.Y && cage.rect.Left < e.X && cage.rect.Right > e.X)
                { 
                    cage.selected = true;
                }
                else
                {
                    cage.selected = false;
                }
            }
            foreach(CageTime cageT in cageTimes)
            {
                if (cageT.rect.Top < e.Y && cageT.rect.Bottom > e.Y && cageT.rect.Left < e.X && cageT.rect.Right > e.X)
                {
                    cageT.selected = true;
                }
                else
                {
                    cageT.selected = false;
                }
            }
        }

        public bool ForTask(Point e)
        {
            if (field[0].rect.Right > e.X)
                return true;
            else
                return false;
        }

        public void CreateTask(Point e, Graphics g, string nameTask, int time, Color colorRect)
        {
            for(int i=0; i<field.Count; i++)
            { 
                if (field[0].rect.Right > e.X)
                {
                    if (field[i].rect.Top < e.Y && field[i].rect.Bottom > e.Y && field[i].rect.Left < e.X && field[i].rect.Right > e.X)
                    {
                        field[i].selected = true;
                        field[i].isTask = true;

                        if (!cageTimes.Any())
                        {
                            SolidBrush br = new SolidBrush(colorRect);
                            cageT = new CageTime(field[i + i / 20 + 1].rect.X, field[i + i / 10 + 1].rect.Y, time, br);
                            cageTimes.Add(cageT);
                            timeOfProject += cageT.timeTask;
                        }
                        else
                        {
                            SolidBrush br = new SolidBrush(colorRect);
                            cageT = new CageTime(cageTimes[cageTimes.Count - 1].rect.X + cageTimes[cageTimes.Count - 1].rect.Width, field[i + i / 20 + 1].rect.Y, time, br);
                            cageTimes.Add(cageT);
                            timeOfProject += cageT.timeTask;
                        }

                        field[i].ToTask(g, nameTask);
                    }
                    else
                    {
                        field[i].selected = false;
                    }
                }
            }
        }

        public void Move(int e)
        {
            foreach (Cage cage in field)
            {
                if (!cage.isTask)
                {
                    cage.rect.X += e;
                    if (field[1].rect.X > 0 + field[1].rect.Width)
                    {
                        cage.rect.X -= e;
                        return;
                    }
                }
            }
            foreach (CageTime cageT in cageTimes)
            {
                cageT.rect.X += e;
            }
        }

        public void Resize(Point e, int dX)
        {
            for (int i = 0; i < cageTimes.Count; i++)
            {
                if(cageTimes[i].inBorder(e))
                {
                    cageTimes[i].rect.Width += dX;
                    cageTimes[i].reTime();
                }
            }
            for(int i=0; i<cageTimes.Count; i++)
            {
                if (i != cageTimes.Count - 1)
                    cageTimes[i + 1].rect.X = cageTimes[i].rect.X + cageTimes[i].rect.Width;
            }
            
        }

        public void NewTimeOfProject()
        {
            timeOfProject = 0;
            for(int i=0; i<cageTimes.Count; i++)
            {
                timeOfProject += cageTimes[i].timeTask;
            }
        }
    }
}
