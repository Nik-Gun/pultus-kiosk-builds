using System.Collections.Generic;

public class SessionManager
{
    public Session currentSession;
    private static SessionManager instance;

    public static SessionManager getInstance()
    {
        return instance ?? (instance = new SessionManager());
    }

    SessionManager()
    {
        currentSession = newSession();
    }

    public Session newSession()
    {
        currentSession = new Session();
        return currentSession;
    }
}