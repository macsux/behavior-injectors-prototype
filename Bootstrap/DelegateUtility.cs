using System;

namespace InjectionModule
{
    public static class DelegateUtility
    {
        public static T Cast<T>(Delegate source) where T : class
        {
            return Cast(source, typeof(T)) as T;
        }

        public static Delegate Cast(Delegate source, Type type)
        {
            if (source == null)
                return null;
            var delegates = source.GetInvocationList();
            if (delegates.Length == 1)
                return Delegate.CreateDelegate(type,
                    delegates[0].Target, delegates[0].Method);
            var delegatesDest = new Delegate[delegates.Length];
            for (var nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
                delegatesDest[nDelegate] = Delegate.CreateDelegate(type,
                    delegates[nDelegate].Target, delegates[nDelegate].Method);
            return Delegate.Combine(delegatesDest);
        }
    }
}