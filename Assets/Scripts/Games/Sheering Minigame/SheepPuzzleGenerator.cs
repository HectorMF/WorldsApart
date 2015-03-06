using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.SheeringMinigame
{
    public class SheepPuzzleGenerator : MonoBehaviour
    {
        public int rows;
        public int columns;
        public int difficulty;
        public int difficultyModifer = 50;

        private List<List<bool>> puzzles;
        private List<List<int>> paths;
        int pathCounter;

        void Start()
        {
            puzzles = new List<List<bool>>();
            paths = new List<List<int>>();
            pathCounter = 0;

            var puzzle = NextGenerationPuzzle();
        }

        public void GeneratePuzzles()
        {
            for (int i = 0; i < 10; i++)
            {
                puzzles.Add(new List<bool>());
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        int check = UnityEngine.Random.Range(0, 2);
                        bool sheerable = check == 1;
                        puzzles[i].Add(sheerable);
                    }
                }
            }
        }

        public void GeneratePathes(List<bool> puzzle)
        {
            for (int k = 0; k < 10; k++)
            {
                var indices = puzzle.Where(x => x).Select(t => puzzle.IndexOf(t)).ToList();

                paths.Add(new List<int>());

                for (int i = 0; i < indices.Count; i++)
                {
                    var selectedIndex = UnityEngine.Random.Range(0, indices.Count());
                    paths[k].Add(indices[selectedIndex]);
                    indices.RemoveAt(selectedIndex);
                }
            }
        }

        public int GradePath(List<int> path)
        {
            int grade = 0;
            for (int i = 1; i < path.Count(); i++)
            {
                if (path[i] % columns != 0 && path[i] == path[i - 1] - 1)
                {
                    grade++;
                    continue;
                }
                if (path[i] % columns != columns - 1 && path[i] == path[i - 1] + 1)
                {
                    grade++;
                    continue;
                }
                if (path[i] > columns - 1 && path[i] == path[i - 1] - columns)
                {
                    grade++;
                    continue;
                }
                if (path[i] < (rows - 1) * columns && path[i] == path[i - 1] + columns)
                {
                    grade++;
                    continue;
                }
            }

            return grade;
        }

        public int NextGenerationPath()
        {
            while (true)
            {
                pathCounter++;
                List<int> grade = paths.Select(p => GradePath(p)).ToList();

                if (grade.Max() == paths[0].Count())
                    return pathCounter;

                List<List<int>> children = new List<List<int>>();
                for (int i = 0; i < 5; i++)
                {
                    children.Add(new List<int>());
                    children.Add(new List<int>());

                    #region Select parent 1
                    int sum = grade.Sum();
                    int select = UnityEngine.Random.Range(0, sum);

                    int parent1 = 0;
                    while (select > 0)
                    {
                        select -= grade[parent1];
                        parent1++;
                    }
                    #endregion

                    #region Select parent 2
                    sum = grade.Sum();
                    select = UnityEngine.Random.Range(0, sum);

                    int parent2 = 0;
                    while (select > 0)
                    {
                        select -= grade[parent2];
                        parent2++;
                    }
                    #endregion

                    //Crossover
                    int crossoverPoint = paths[0].Count();

                    for (int k = 0; k < paths[0].Count(); k++)
                    {
                        if (k < crossoverPoint)
                        {
                            children[i * 2].Add(paths[parent1][k]);
                            children[i * 2 + 1].Add(paths[parent2][k]);
                        }
                        else
                        {
                            children[i * 2].Add(paths[parent2][k]);
                            children[i * 2 + 1].Add(paths[parent1][k]);
                        }
                    }

                    //Handle Duplicates
                    var duplicates1 = children[i * 2].GroupBy(a => a).Where(group => group.Count() > 1).Select(t => t.Key).ToList();
                    var duplicates2 = children[i * 2].GroupBy(a => a).Where(group => group.Count() > 1).Select(t => t.Key).ToList();

                    while (duplicates1.Count > 0)
                    {
                        var duplicate1 = duplicates1[0];
                        var duplicate2 = duplicates2[0];
                        children[i * 2][children[i * 2].FindIndex(t => t == duplicate1)] = duplicate2;
                        children[i * 2 + 1][children[i * 2 + 1].FindIndex(t => t == duplicate2)] = duplicate1;
                        duplicates1.RemoveAt(0);
                        duplicates2.RemoveAt(0);
                    }

                    //Mutate
                    var index1 = UnityEngine.Random.Range(0, children[0].Count());
                    var index2 = UnityEngine.Random.Range(0, children[0].Count());
                    while (index1 == index2)
                        index2 = UnityEngine.Random.Range(0, children[0].Count());

                    var temp = children[i * 2 + 1][index1];
                    children[i * 2 + 1][index1] = children[i * 2 + 1][index2];
                    children[i * 2 + 1][index2] = temp;
                }

                paths = children;
            }
        }

        public List<bool> NextGenerationPuzzle()
        {
            while (true)
            {
                GeneratePuzzles();

                List<int> grade = new List<int>();

                foreach (List<bool> p in puzzles)
                {
                    GeneratePathes(p);
                    grade.Add(NextGenerationPath());
                }

                if (grade.Max() == difficulty * difficultyModifer)
                    return puzzles[grade.IndexOf(grade.Max())];

                List<List<bool>> children = new List<List<bool>>();
                for (int i = 0; i < 5; i++)
                {
                    children.Add(new List<bool>());
                    children.Add(new List<bool>());

                    #region Select parent 1
                    int sum = grade.Sum();
                    int select = UnityEngine.Random.Range(0, sum);

                    int parent1 = 0;
                    while (select > 0)
                    {
                        select -= grade[parent1];
                        parent1++;
                    }
                    #endregion

                    #region Select parent 2
                    sum = grade.Sum();
                    select = UnityEngine.Random.Range(0, sum);

                    int parent2 = 0;
                    while (select > 0)
                    {
                        select -= grade[parent2];
                        parent2++;
                    }
                    #endregion

                    //Crossover
                    int crossoverPoint = puzzles[0].Count();

                    for (int k = 0; k < puzzles[0].Count(); k++)
                    {
                        if (k < crossoverPoint)
                        {
                            children[i * 2].Add(puzzles[parent1][k]);
                            children[i * 2 + 1].Add(puzzles[parent2][k]);
                        }
                        else
                        {
                            children[i * 2].Add(puzzles[parent2][k]);
                            children[i * 2 + 1].Add(puzzles[parent1][k]);
                        }
                    }
                }
            }
        }
    }
}
