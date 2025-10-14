using GridScheduleSample;
using Syncfusion.Schedule;
using Syncfusion.Windows.Forms.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleControlDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SimpleScheduleDataProvider data;

            if (File.Exists("default.schedule"))
            {
                data = SimpleScheduleDataProvider.LoadBinary("default.schedule");
                data.FileName = "default.schedule";
            }

            else
            {
                data = new SimpleScheduleDataProvider();
                data.MasterList = new SimpleScheduleAppointmentList();
                data.FileName = "default.schedule";
            }         

            ScheduleAppointment item = data.MasterList.NewScheduleAppointment() as ScheduleAppointment;
            item.StartTime = DateTime.Now;
            item.EndTime = item.StartTime.AddDays(2);

            //item.BackColor = Color.Green;
            item.ForeColor = Color.Red;
            data.MasterList.Add(item);

            this.scheduleControl1.ScheduleType = ScheduleViewType.CustomWeek;
            this.scheduleControl1.DataSource = data;
            this.scheduleControl1.Calendar.SelectedDates.BeginUpdate();
            this.scheduleControl1.Calendar.SelectedDates.Clear();
            for (int i = 0; i < 7; i++)
            {
               // Add 7 days and start day.
               this.scheduleControl1.Calendar.SelectedDates.Add(DateTime.Now.StartOfWeek(DayOfWeek.Sunday).AddDays(i));
            }
            this.scheduleControl1.Calendar.SelectedDates.EndUpdate();
        }
    }
    
    public static class Extensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
