using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        private GameManager gameManager;
        private Player player;

        [SetUp]
        public void Setup()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
            GameObject clone = Object.Instantiate(prefab);
            gameManager = clone.GetComponent<GameManager>();
            player = Object.FindObjectOfType<Player>();
        }
        #region Unit Tests
        [UnityTest]
        public IEnumerator GameManagerWasLoaded()
        {
            yield return new WaitForEndOfFrame();
            //Asset that item should be destroyed(non-existent)
            Assert.IsTrue(gameManager != null);
        }
        [UnityTest]
        public IEnumerator PlayerExistsInGame()
        {
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(player != null);
        }
        [UnityTest]
        public IEnumerator ItemCollidesWithPlayer()
        {

            //Get the player

            Item item = Object.FindObjectOfType<Item>();
            player.transform.position = new Vector3(0, 2, 0);
            item.transform.position = new Vector3(0, 2, 0);

            yield return new WaitForSeconds(0.1f);

            //Assert that item shoul be destroyed 
            Assert.IsTrue(item == null);
        }

        [UnityTest]
        public IEnumerator PlayerShootsItem()
        {
            Player player = Object.FindObjectOfType<Player>();
            Item item = Object.FindObjectOfType<Item>();
            player.transform.position = new Vector3(0, 3, -3);
            item.transform.position = new Vector3(0, 3, 0);

            yield return null;

            Assert.IsTrue(player.Shoot());

        }
        [UnityTest]
        public IEnumerator ItemCollectionAddsOneScore()
        {
            // Find any item
            Item item = Object.FindObjectOfType<Item>();

            // Record old score
            int oldScore = gameManager.score;

            // Move player on top of item
            player.transform.position = new Vector3(0, 2, 0);
            item.transform.position = new Vector3(0, 2, 0);

            // Wait a few seconds (or until end of Frame)
            yield return new WaitForSeconds(0.1f);

            // GameManager should have 1 score added
            Assert.IsTrue(gameManager.score == oldScore + 1);
        }
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(gameManager.gameObject);
        }
    }
    #endregion
}
