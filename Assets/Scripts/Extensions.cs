using UnityEngine;

static class Extensions
{
    public static void Shuffle(this GridObject[] positions)
    {
        int placeOld, placeNew;
        GridObject element;

        for (int i = 0; i < positions.Length; i++)
        {
            placeOld = Random.Range(0, positions.Length);
            placeNew = Random.Range(0, positions.Length);
            element = positions[placeOld];
            positions[placeOld] = positions[placeNew];
            positions[placeNew] = element;
        }
    }
}
