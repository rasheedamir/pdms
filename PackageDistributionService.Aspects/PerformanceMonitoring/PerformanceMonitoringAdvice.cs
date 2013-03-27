using System;
using AopAlliance.Intercept;
using log4net;

namespace PackageDistributionService.Aspects.PerformanceMonitoring
{
    /// <summary>
    /// Around advice to measure the performance of service calls.
    /// 
    /// Around Advice
    /// The most powerful type of advice you will look at is also the root type of advice on 
    /// which the other types of advice are built. Around advice does not specify where your 
    /// advice will be run in relation to a method; instead, it wraps the method execution 
    /// and allows you to control where around the method execution you want to run your 
    /// advice code.
    /// 
    /// http://www.springframework.net/doc-latest/reference/html/aop.html
    /// </summary>
    public class PerformanceMonitoringAdvice : IMethodInterceptor 
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PerformanceMonitoringAdvice));

        public object Invoke(IMethodInvocation invocation)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            // Start timing the method
            stopwatch.Start();
            try
            {
                // Execute the method
                return invocation.Proceed();
            }
            finally
            {
                // Stop timing the method
                stopwatch.Stop();
                // Log method runtime
                Log.Debug(String.Format("Method Name: {0} took: {1} milliseconds to run.", invocation.Method.Name, stopwatch.ElapsedMilliseconds));                
            }
        }
    }
}
