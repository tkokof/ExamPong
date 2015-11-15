// desc assert util class for better defending programming
// maintainer hugoyu

using System;
using UnityEngine;

public class AssertUtil {

    public static void Assert(bool condition, string expression) {
        if (!condition) {
            Debug.LogError(expression);
            throw new Exception(expression);
        }
    }

    public static void Assert(bool condition) {
        Assert(condition, "Assert Fail!");
    }

}
