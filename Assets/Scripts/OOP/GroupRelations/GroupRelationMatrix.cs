using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroupRelation
{
    NEUTRAL,
    ENEMY
}

public class GroupRelationMatrix
{
    GroupRelation[,] _relMatrix;

    public GroupRelationMatrix()
    {
        int noneIdx = (int)CharacterGroupId.NONE;
        int dim = System.Enum.GetValues(typeof(CharacterGroupId)).Length;
        _relMatrix = new GroupRelation[dim, dim];
        for(int i = 0; i < dim; i++)
        {
            for(int j = 0; j < dim; j++)
            {
                if (i == j || i == noneIdx || j == noneIdx) _relMatrix[i, j] = GroupRelation.NEUTRAL;
                else _relMatrix[i, j] = GroupRelation.ENEMY;
            }
        }
    }

    public GroupRelation[] GetRelations(CharacterGroupId row)
    {
        return SliceRow((int)row).ToArray();
    }

    private IEnumerable<GroupRelation> SliceRow(int row)
    {
        for (int i = 0; i < _relMatrix.GetLength(0); i++)
            yield return _relMatrix[row, i];
    }
}
