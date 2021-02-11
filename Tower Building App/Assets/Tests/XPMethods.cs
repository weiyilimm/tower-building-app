using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    // Roy Osherove's naming strategy
    // MethodName_StateUnderTest_ExpectedBehavior

    public class XPMethods
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TimerXP_TimeReturnsCorrectXP_TimeEqualsXP()
        {
            //"Run timer" - set timer value
            //get XP for this value
            //test if equivilant
            int time;

            //Seconds
            time = 22;
            Assert.AreEqual(time, 22);

            //Minutes
            time = 458;
            Assert.AreEqual(time, 458);

            //Hour
            time = 3600;
            Assert.AreEqual(time, 3600);
        }

        [Test]
        public void TimerDisplay_TimerHourMinuteSecondMatchesSecondsConversion_TimeEqualsExpectedTimeSeconds()
        {
            int time;

            //Seconds
            time = 22;
            Assert.AreEqual(time, 22);

            //Minutes
            time = 458;
            Assert.AreEqual(time, 458);

            //Hour
            time = 3600;
            Assert.AreEqual(time, 3600);
        }


        /*
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator XPMethodsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */
    }
}
