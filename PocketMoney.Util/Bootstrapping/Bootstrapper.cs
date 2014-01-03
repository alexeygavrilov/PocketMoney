using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.Util.Bootstrapping
{
    public class Bootstrapper : Disposable
    {
        private static readonly Bootstrapper instance = new Bootstrapper();

        private readonly IList<Type> taskTypes = new List<Type>();

        private IList<BootstrappingTask> tasks;

        private Bootstrapper()
        {
        }

        public static Bootstrapper Instance
        {
            get { return instance; }
        }

        public Bootstrapper Include<TTask>() where TTask : BootstrappingTask
        {
            Type taskType = typeof (TTask);

            if (!taskTypes.Contains(taskType))
            {
                taskTypes.Add(taskType);
            }

            return this;
        }

        public void Init()
        {
            tasks = taskTypes.Select(type => (BootstrappingTask) ServiceLocator.Current.GetInstance(type)).ToList();

            foreach (BootstrappingTask task in tasks)
            {
                task.Execute();
            }
        }

        protected override void DisposeCore()
        {
            if (tasks != null)
            {
                foreach (BootstrappingTask task in tasks.Reverse())
                {
                    task.Dispose();
                }

                tasks.Clear();
            }

            taskTypes.Clear();
        }
    }
}