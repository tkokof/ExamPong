// desc input base class
// maintainer hugoyu

class InputData {

    public enum InputType {
        
        Fire,
        Move,
        Unknown,

    }

    InputType type;
    Paddle target;
    float[] param;

    static InputData s_inputDataBuffer = new InputData();
    public static InputData GetInputData(InputType type_, Paddle target_, params float[] param_) {
        s_inputDataBuffer.type = type_;
        s_inputDataBuffer.target = target_;
        s_inputDataBuffer.param = param_;

        return s_inputDataBuffer;
    }

    public InputData(InputType type_ = InputType.Unknown, Paddle target_ = null, params float[] param_) {
        type = type_;
        target = target_;
        param = param_;
    }

    public InputType GetInputType() {
        return type;
    }

    public Paddle GetTarget() {
        return target;
    }

    public float[] GetParams() {
        return param;
    }

    public float GetParamByIndex(int index) {
        AssertUtil.Assert(index >= 0 && index < param.Length);
        return param[index];
    }

}