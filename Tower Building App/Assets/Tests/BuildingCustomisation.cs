using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    // Roy Osherove's naming strategy
    // MethodName_StateUnderTest_ExpectedBehavior

    public class BuildingCustomisation
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ExpectedMaterials_MaterialCallGetsMaterial_True()
        {
            //get material
            //is material expected material

            //matte white
            Assert.IsTrue(true);

            //matte red
            Assert.IsTrue(true);
        }

        [Test]
        public void ExpectedBuilding_ModelIDGetsModel_True()
        {
            Assert.IsTrue(true);
        }

        /*
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */
    }
}
