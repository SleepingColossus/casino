using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;
    public Transform spawnArea;

    public float spawnInterval;
    private float _elapsedSinceSpawn;
    private bool _readyToSpawn;

    private Vector3 _position;
    private Vector3 _scale;

    private int _coinsToSpawn;

    void Start()
    {
        _readyToSpawn = false;
        _elapsedSinceSpawn = 0;

        _position = spawnArea.position;
        _scale = spawnArea.localScale;
    }

    void Update()
    {
        if (_coinsToSpawn > 0)
        {
            if (_readyToSpawn)
            {
                Spawn();
                _readyToSpawn = false;
            }
            else
            {
                if (_elapsedSinceSpawn > spawnInterval)
                {
                    _readyToSpawn = true;
                    _elapsedSinceSpawn = 0;
                }
            }

            _elapsedSinceSpawn += Time.deltaTime;
        }
    }

    private void Spawn()
    {
        float x = Random.Range(_position.x - 0.5f, _position.x + 0.5f);
        float z = Random.Range(_position.z - 0.5f, _position.z + 0.5f);

        var position = new Vector3(x, _position.y, z);
        Instantiate(coin, position, Quaternion.identity);
        _coinsToSpawn--;
    }

    public void Spawn(int coinsToSpawn)
    {
        _coinsToSpawn = coinsToSpawn;
        _readyToSpawn = true;
    }
}
