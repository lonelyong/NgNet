using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet
{
    public static class TaskHelper
    {        /// <summary>
             /// 获取一个值，该值指示当前线程是否正在执行查询工作
             /// </summary>
             /// <param name="thread"></param>
             /// <returns></returns>
        public static bool ThreadIsRunning(System.Threading.Thread thread)
        {
            if (thread == null)
                return false;
            else if (thread.ThreadState == System.Threading.ThreadState.Aborted
                || thread.ThreadState == System.Threading.ThreadState.Stopped
                || thread.ThreadState == System.Threading.ThreadState.Unstarted)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 获取一个值，该值指示task是否正在运行或等待子任务运行或将要运行
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static bool TaskIsRunning(System.Threading.Tasks.Task task)
        {
            if (task == null)
                return false;
            if (task.Status == TaskStatus.Running || task.Status == TaskStatus.WaitingForChildrenToComplete || task.Status == TaskStatus.WaitingForActivation || task.Status == TaskStatus.WaitingToRun)
                return true;
            else
                return false;
        }
    }
}
