using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
  #region Module
  public class TestSuite
  {
    private GameManager gameManager;
    private Player player;

    [SetUp] public void Setup()
    {
      GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
      GameObject clone = Object.Instantiate(prefab);
      gameManager = clone.GetComponent<GameManager>();
      player = Object.FindObjectOfType<Player>();
    }
    #region Unit Tests
    [UnityTest] public IEnumerator GameManagerWasLoaded()
    {
      yield return new WaitForEndOfFrame();

      // Check if it exists after frame
      Assert.IsTrue(gameManager != null);
    }
    [UnityTest] public IEnumerator PlayerExistsInGame()
    {
      yield return new WaitForEndOfFrame();

      Assert.IsTrue(player != null);
    }
    [UnityTest] public IEnumerator ItemCollidesWithPlayer()
    {
      // Get an item
      Item item = Object.FindObjectOfType<Item>();

      // Position both in the same location
      player.transform.position = new Vector3(0, 2, 0);
      item.transform.position = new Vector3(0, 2, 0);
           
      yield return new WaitForSeconds(0.1f);

      // Asset that item should be destroyed (non-existent)
      Assert.IsTrue(item == null);
    }
    [UnityTest] public IEnumerator PlayerShootsItem()
    {
      Item item = Object.FindObjectOfType<Item>();

      player.transform.position = new Vector3(0, 2, -2);
      item.transform.position = new Vector3(0, 2, 0);

      yield return null;

      Assert.IsTrue(player.Shoot());
      // Assert.IsTrue(item == null);
    }
    [UnityTest] public IEnumerator ItemCollectionAddsOneScore()
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
    #endregion
    [TearDown] public void Teardown()
    {
      Object.Destroy(gameManager.gameObject);
    }
  }
  #endregion
}