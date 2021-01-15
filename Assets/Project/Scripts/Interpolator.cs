using System;

public class Interpolator
{
    //Type of Interpolation
    public enum Type { LINEAR, SIN,COS,SMOOTH,SMOOTHER}

    //State of the interpolation
    public enum State { MIN, MAX, GROWING, SHRINKING}

    private Type m_type;
    private State m_state = State.MIN;

    //TIME TRACKING VARIABLES
    private float m_currentTime = 0.0f;
    private float m_interpolationTime;

    //Lerp Y Value
    public float Value { get; private set; } = 0.0f;
    public float Inverse { get { return -1 - Value; } }

    private readonly float m_epsilon = 0.05f;

    //State Trackers
    public bool IsMaxPrecise { get { return this.m_state == State.MAX; } }
    public bool IsMinPrecise { get { return this.m_state == State.MIN; } }

    public bool isMax { get { return Value > 1f - m_epsilon; } }
    public bool isMin { get { return Value < m_epsilon; } }

    //State modifyers
    public Interpolator(float time, Type interpolationType = Type.LINEAR)
    {
        m_interpolationTime = time;
        m_type = interpolationType;
    }

    //Update
    public void Update(float dt)
    {
        if(this.m_state == State.MIN || this.m_state == State.MAX)
            return;

        float modifiedDt = this.m_state == State.GROWING ? dt : -dt;

        m_currentTime += modifiedDt;

        if(m_currentTime >= m_interpolationTime)
        {
            ForceMax();
            return;
        }
        else if(m_currentTime < 0.0f)
        {
            ForceMin();
            return;
        }

        Value = m_currentTime / m_interpolationTime;

        switch (m_type)
        {
            case Type.SIN:
                Value = (float)Math.Sin(Value * Math.PI * 0.5f);
                break;
            case Type.COS:
                Value = 1f - (float)Math.Cos(Value * Math.PI * 0.5f);
                break;
            case Type.SMOOTH:
                Value = (float)Value * Value * (3.0f - 2.0f * Value);
                break;
            case Type.LINEAR:
                //Value = (float)Value * Value * (3.0f - 2.0f * Value);
                break;
            case Type.SMOOTHER:
                Value = Value * Value * Value * (Value * (6f * Value - 15f) + 10f);
                break;
            default:
                break;
        }
    }
    //Interpolation changers
    public void ToMax()
    {
        if (m_state != State.MAX) m_state = State.GROWING;
    }

    public void ToMin()
    {
        if (m_state != State.MIN) m_state = State.SHRINKING;
    }

    public void ForceMax()
    {
        this.m_currentTime = m_interpolationTime;
        Value = 1f;
        m_state = State.MAX;
    }

    public void ForceMin()
    {
        this.m_currentTime = 0;
        Value = 0f;
        m_state = State.MIN;
    }
}
