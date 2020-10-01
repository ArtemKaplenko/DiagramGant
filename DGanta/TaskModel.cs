using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGanta
{
    class TaskModel
    {
        public string taskName { get; set; }
        public float time { get; set; }
    }
    class TaskListModel
    {
        static string[] names =
        {
            "1.Отобразить список задачь",
            "2.Отобразить время на каждую задачу",
            "3.Отобразить прямоугольник",
            "4.Рассчитать масштаб времени в попугаях PAR",
            "5.Отобразить масштабную сетку",
            "6.Отобразить временную шкалу",
            "7.Пересчитать временную шкалу"
        };
        static float[] times =
        {
            1,
            0.3f,
            2,
            0.5f,
            0.5f,
            0.5f,
            0.2f
        };
        public List<TaskModel> data { get; set; }
        public TaskListModel()
        {
            data = new List<TaskModel>();
            foreach (string i in names)
                data.Add(new TaskModel() { taskName = i });
        }
    }
}
