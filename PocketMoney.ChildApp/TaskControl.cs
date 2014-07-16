using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results.Clients;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.Windows.Forms;

namespace PocketMoney.ChildApp
{
    public partial class TaskControl : UserControl
    {
        private TaskView _task = null;
        private GoalView _goal = null;
        private bool _isGrabTask = false;

        public event EventHandler OnClose;
        public event EventHandler OnProcess;

        public TaskControl()
        {
            InitializeComponent();
        }

        public void OpenGoal(GoalView goal)
        {
            buttonDone.Text = "I have reached this goal!";
            labelCustom.Visible = false;
            textCustom.Visible = false;
            textNote.Height = 92;
            _goal = goal;
            textNote.Text = string.Empty;
            textTitle.Text = _goal.Text;
            textReward.Text = _goal.Reward;
            this.Visible = true;
        }

        public void OpenTask(TaskView task)
        {
            textNote.Height = 48;
            labelCustom.Visible = true;
            textCustom.Visible = true;
            if (_isGrabTask)
            {
                buttonDone.Text = "Grabb it!";
            }
            else
            {
                buttonDone.Text = "I done it!";
            }
            _task = task;
            textNote.Text = string.Empty;
            textTitle.Text = _task.Title;
            textReward.Text = _task.Reward;
            textDescription.Text = _task.Text;
            switch (_task.TaskType)
            {
                case TaskType.CLEAN_TYPE:
                    labelCustom.Text = "Room:";
                    textCustom.Text = ((CleanTaskView)_task).RoomName;
                    break;
                case TaskType.HOMEWORK_TYPE:
                    labelCustom.Text = "Lesson:";
                    textCustom.Text = ((HomeworkTaskView)_task).Lesson;
                    break;
                case TaskType.ONE_TIME_TYPE:
                    labelCustom.Text = "Name:";
                    textCustom.Text = ((OneTimeTaskView)_task).Name;
                    break;
                case TaskType.REPEAT_TYPE:
                    labelCustom.Text = "Name:";
                    textCustom.Text = ((RepeatTaskView)_task).Name;
                    break;
                case TaskType.SHOPPING_TYPE:
                    labelCustom.Text = "Shop:";
                    textCustom.Text = ((ShoppingTaskView)_task).ShopName;
                    break;
            }
            this.Visible = true;
        }

        public void CloseTask()
        {
            _goal = null;
            _task = null;
            this.Visible = false;
            if (this.OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            CloseTask();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            var clientService = ServiceLocator.Current.GetInstance<IClientService>();
            Result result = null;
            if (_task != null)
            {
                if (_isGrabTask)
                {
                    if (MessageBox.Show(this.Parent, "Are you sure to want Grabb this task?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        result = clientService.GrabbTask(new ProcessRequest { Id = _task.Id, Note = textNote.Text });
                    }
                }
                else
                {
                    if (MessageBox.Show(this.Parent, "Have you made this task?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        result = clientService.DoneTask(new DoneTaskRequest { Id = _task.Id, Note = textNote.Text, DateType = _task.DateType });
                    }
                }
            }
            else if (_goal != null)
            {
                if (MessageBox.Show(this.Parent, "Have you reached this goal?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    result = clientService.AchieveGoal(new ProcessRequest { Id = _goal.Id, Note = textNote.Text });
                }
            }

            if (result != null)
            {
                if (result.Success)
                {
                    if (this.OnProcess != null)
                        this.OnProcess(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


    }
}
