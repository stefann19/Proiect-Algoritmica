using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Proiect_Algoritmica.Scripts.GraphEditor;

namespace Proiect_Algoritmica.Scripts.Graphs
{
    public class TreeParsings
    {
        #region Parsings

        public static string GenericParsing(Graph graph, Node s)
        {
            Dictionary<int, Node>
                unvisitedNodes = new Dictionary<int, Node>(graph.Nodes); //U := N − {s};
            unvisitedNodes.Remove(s.NodeIndex);


            Dictionary<int, Node> vAuaNodes = new Dictionary<int, Node> {{s.NodeIndex, s}}; //V := {s};

            Dictionary<int, Node> wAANodes = new Dictionary<int, Node>(); //W := multimea vida ; 

            Dictionary<int, int> predecesorsList = new Dictionary<int, int>();
            Dictionary<int, int> orderList = new Dictionary<int, int>();
            foreach (Node nodesValue in graph.Nodes.Values)
            {
                predecesorsList.Add(nodesValue.NodeIndex, 0);
                orderList.Add(nodesValue.NodeIndex, int.MaxValue);
            }
            orderList[s.NodeIndex] = 1;
            int k = 1;

            while (vAuaNodes.Count > 0)
            {
                Node x = vAuaNodes.First().Value;
                x.Roads.Keys.ToList().Where(key => unvisitedNodes.Values.ToList().Any(key.Equals)).ToList().ForEach(
                    y =>
                    {
                        unvisitedNodes?.Remove(y.NodeIndex);
                        vAuaNodes.Add(y.NodeIndex, y);
                        predecesorsList[y.NodeIndex] = x.NodeIndex;
                        k++;
                        orderList[y.NodeIndex] = k;
                    });
                vAuaNodes.Remove(x.NodeIndex);
                wAANodes.Add(x.NodeIndex, x);
            }
            string Result = "";

            Result = "p={";
            predecesorsList.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}\no={";
            orderList.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}";
            return Result.Replace(int.MaxValue.ToString(), "∞");
        }

        public static string BreadthFirstParsing(Graph graph, Node s)
        {
            Dictionary<int, Node>
                unvisitedNodes = new Dictionary<int, Node>(graph.Nodes); //U := N − {s};
            unvisitedNodes.Remove(s.NodeIndex);


            Dictionary<int, Node> vAuaNodes = new Dictionary<int, Node> {{s.NodeIndex, s}}; //V := {s};

            Dictionary<int, Node> wAANodes = new Dictionary<int, Node>(); //W := multimea vida ; 

            Dictionary<int, int> predecesorsList = new Dictionary<int, int>();
            Dictionary<int, int> lengthList = new Dictionary<int, int>();
            foreach (Node nodesValue in graph.Nodes.Values)
            {
                predecesorsList.Add(nodesValue.NodeIndex, 0);
                lengthList.Add(nodesValue.NodeIndex, int.MaxValue);
            }
            lengthList[s.NodeIndex] = 0;


            while (vAuaNodes.Count > 0)
            {
                Node x = vAuaNodes.First().Value;
                x.Roads.Keys.ToList().Where(key => unvisitedNodes.Values.ToList().Any(key.Equals)).ToList().ForEach(
                    y =>
                    {
                        unvisitedNodes?.Remove(y.NodeIndex);
                        vAuaNodes.Add(y.NodeIndex, y);
                        predecesorsList[y.NodeIndex] = x.NodeIndex;
                        lengthList[y.NodeIndex] = lengthList[x.NodeIndex] + 1;
                    });
                vAuaNodes.Remove(x.NodeIndex);
                wAANodes.Add(x.NodeIndex, x);
            }

            string Result = "";

            Result = "p={";
            predecesorsList.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}\nl={";
            lengthList.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}";
            return Result.Replace(int.MaxValue.ToString(), "∞");
        }

        public static string DepthFirstParsing(Graph graph, Node s)
        {
            Dictionary<int, Node>
                unvisitedNodes = new Dictionary<int, Node>(graph.Nodes); //U := N − {s};
            unvisitedNodes.Remove(s.NodeIndex);


            Dictionary<int, Node> vAuaNodes = new Dictionary<int, Node> {{s.NodeIndex, s}}; //V := {s};

            Dictionary<int, Node> wAANodes = new Dictionary<int, Node>(); //W := multimea vida ; 

            Dictionary<int, int> predecesorsList = new Dictionary<int, int>();
            Dictionary<int, int> timeList1 = new Dictionary<int, int>();
            Dictionary<int, int> timeList2 = new Dictionary<int, int>();
            foreach (Node nodesValue in graph.Nodes.Values)
            {
                predecesorsList.Add(nodesValue.NodeIndex, 0);
                timeList1.Add(nodesValue.NodeIndex, int.MaxValue);
                timeList2.Add(nodesValue.NodeIndex, int.MaxValue);
                //orderList.Add(nodesValue.NodeIndex, int.MaxValue);
            }

            timeList1[s.NodeIndex] = 1;
            timeList2[s.NodeIndex] = 1;

            int t = 1;
            while (vAuaNodes.Count > 0)
            {
                Node x = vAuaNodes.Last().Value;
                x.Roads.Keys.ToList().Where(key => unvisitedNodes.Values.ToList().Any(key.Equals)).ToList().ForEach(
                    y =>
                    {
                        unvisitedNodes?.Remove(y.NodeIndex);
                        vAuaNodes.Add(y.NodeIndex, y);
                        predecesorsList[y.NodeIndex] = x.NodeIndex;
                        t = t + 1;
                        timeList1[y.NodeIndex] = t;
                    });
                vAuaNodes.Remove(x.NodeIndex);
                wAANodes.Add(x.NodeIndex, x);
                t++;
                timeList2[x.NodeIndex] = t;
            }

            string Result = "";

            Result = "p={";
            predecesorsList.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}\nt1={";
            timeList1.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}\tt2={";
            timeList2.Values.ToList().ForEach(p => { Result += p.ToString() + ","; });
            Result = Result.Substring(0, Result.Length - 1);
            Result += "}";
            return Result.Replace(int.MaxValue.ToString(), "∞");
        }

        #endregion

        #region Trees

        public static List<Road> GenericTree(Graph graph)
        {
            Dictionary<Node, List<Node>> N = new Dictionary<Node, List<Node>>();
            Dictionary<List<Node>, List<Road>> A = new Dictionary<List<Node>, List<Road>>();

            List<Road> Aa = new List<Road>();

            graph.Nodes.Values.ToList().ForEach(node =>
            {
                N.Add(node, new List<Node> {node});
                A.Add(N[node], new List<Road>());
            });

            for (int i = 0; i < graph.Nodes.Count - 1; i++)
            {
                KeyValuePair<Node, List<Node>> subtree = N.Where(n => n.Value.Count > 0).ToList().First();

                Node smallestRoadNodeInSubtree = subtree.Value.OrderBy(node =>
                {
                    List<KeyValuePair<Node, Road>> ras = node.Roads
                        .Where(road => !subtree.Value.Any(sn => sn.Equals(road.Key)))
                        .OrderBy(road => road.Value.Cost).ToList();
                    return ras.Count == 0 ? int.MaxValue : ras.First().Value.Cost;
                }).First();

                //Node smallestRoadNodeInSubtree = subtree.Value.OrderBy(n => n.Roads.Values.ToList().OrderBy(no => no.Cost).First().Cost).First();
                List<KeyValuePair<Node, Road>> rList =
                    smallestRoadNodeInSubtree.Roads.Where(road => !subtree.Value.Any(sn => sn.Equals(road.Key)))
                        .ToList();
                if (!rList.Any())
                {
                    if (i == graph.Nodes.Count - 2) Aa = A[subtree.Value];
                    continue;
                }
                Road r = rList.OrderBy(road => road.Value.Cost)
                    .First().Value;

                Node endingNode = smallestRoadNodeInSubtree.Roads.First(a => a.Value == r).Key;
                N[subtree.Key].AddRange(N[endingNode]);
                N[endingNode].Clear();


                A[subtree.Value].Add(r);
                A[subtree.Value].AddRange(A[N[endingNode]]);
                A[N[endingNode]].Clear();

                if (i == graph.Nodes.Count - 2)
                    Aa = A[subtree.Value];
            }
            return Aa;
        }

        public static List<Road> PrimeTree(Graph graph)
        {
            Dictionary<Node, int> V = new Dictionary<Node, int>();
            graph.Nodes.ToList().ForEach(node => { V.Add(node.Value, int.MaxValue); });
            V[graph.Nodes.First().Value] = 0;

            Dictionary<Node, Road> E = new Dictionary<Node, Road>();

            List<Node> N1 = new List<Node>();
            List<Road> A = new List<Road>();
            List<Node> N2 = new List<Node>(graph.Nodes.Values);

            while (N1.Count != graph.Nodes.Count)
            {
                KeyValuePair<Node, int> vy = V.OrderBy(kvp => kvp.Value).First();
                N1.Add(vy.Key);
                N2.Remove(vy.Key);
                if (vy.Key != graph.Nodes.First().Value)
                    A.Add(E[vy.Key]);


                Node smallestRoadNodeInSubtree = N1.OrderBy(node =>
                {
                    List<KeyValuePair<Node, Road>> ras = node.Roads.Where(road => !N1.Any(sn => sn.Equals(road.Key)))
                        .OrderBy(road => road.Value.Cost).ToList();
                    return ras.Count == 0 ? int.MaxValue : ras.First().Value.Cost;
                }).First();

                E = smallestRoadNodeInSubtree.Roads.Where(road => !N1.Any(sn => sn.Equals(road.Key)))
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
                V.Clear();

                E.ToList().ForEach(e =>
                {
                    V.Add(e.Key, (int) e.Value.Cost);
                    //V[e.Key] = (int)e.Value.Cost;
                });
            }

            return A;
        }

        public static List<Road> KruskalTree(Graph graph)
        {
            HashSet<Node> A = new HashSet<Node>();
            List<Road> roads = new List<Road>();
            graph.Roads.OrderBy(road => road.Cost).ToList().ForEach(road =>
            {
                if (ExistsIndirectRoad(roads, road)) return;

                if (!A.Contains(road.StartingNode)) A.Add(road.StartingNode);
                if (!A.Contains(road.EndingNode)) A.Add(road.EndingNode);
                roads.Add(road);
            });

            return roads;
        }

        private static bool ExistsIndirectRoad(List<Road> roads, Road road)
        {
            List<Node> VisitedNodes = new List<Node> {road.StartingNode};
            int lastCount = 0;
            while (lastCount != VisitedNodes.Count)
            {
                lastCount = VisitedNodes.Count;
                roads.Where(r =>
                {
                    int nr = 0;
                    if (VisitedNodes.Any(n => n.Equals(r.StartingNode))) nr++;
                    if (VisitedNodes.Any(n => n.Equals(r.EndingNode))) nr++;
                    return nr == 1;
                }).ToList().ForEach(linkedRoad =>
                {
                    VisitedNodes.Add(linkedRoad.EndingNode);
                    VisitedNodes.Add(linkedRoad.StartingNode);
                });
            }


            return VisitedNodes.Any(r => r.Equals(road.EndingNode));
        }

        #endregion

        #region Roads

        public static List<Road> DijkstraAlgorithm(Graph graph, Node startingNode, Node endingNode)
        {
            HashSet<Node> W = new HashSet<Node>(graph.Nodes.Values);
            Dictionary<Node, double> D = new Dictionary<Node, double>();
            Dictionary<Node, Node> P = new Dictionary<Node, Node>();
            graph.Nodes.Values.ToList().ForEach(node =>
            {
                D.Add(node, int.MaxValue);
                P.Add(node, null);
            });
            D[startingNode] = 0;

            while (W.Count > 0)
            {
                KeyValuePair<Node, double> minimPair =
                    D.Where(d => W.Any(w => w.Equals(d.Key))).OrderBy(d => d.Value).First();
                W.Remove(minimPair.Key);

                minimPair.Key.Roads.Where(road => W.Any(w => w.Equals(road.Key))).ToList().ForEach(road =>
                {
                    if (D[minimPair.Key] + road.Value.Cost < D[road.Key])
                    {
                        D[road.Key] = D[minimPair.Key] + road.Value.Cost;
                        P[road.Key] = minimPair.Key;
                    }
                });
            }

            List<Road> roads = new List<Road>();
            Node currentNode = endingNode;
            while (currentNode != startingNode)
            {
                roads.Add(currentNode.Roads[P[currentNode]]);
                currentNode = P[currentNode];
            }

            return roads;
        }

        public static List<Road> BellmanFordAlgorithm(Graph graph, Node startingNode, Node endingNode)
        {
            HashSet<Node> W = new HashSet<Node>(graph.Nodes.Values);
            Dictionary<Node, double> D = new Dictionary<Node, double>();
            Dictionary<Node, double> D1 = new Dictionary<Node, double>();
            Dictionary<Node, Node> P = new Dictionary<Node, Node>();
            graph.Nodes.Values.ToList().ForEach(node =>
            {
                D.Add(node, int.MaxValue);
                P.Add(node, null);
            });
            D[startingNode] = 0;
            while (!DictionaryEqualityCheck(D, D1))
            {
                D1 = new Dictionary<Node, double>(D);

                graph.Nodes.Values.Where(n => graph.Roads.Any(road => road.EndingNode == n))
                    .ToList().ForEach(node =>
                    {
                        KeyValuePair<Node, Road> kvp = node.Roads.Where(road => road.Value.EndingNode.Equals(node))
                            .OrderBy(road => D1[road.Key] + road.Value.Cost).First();
                        if (D1[kvp.Key] + kvp.Value.Cost < D1[node])
                        {
                            D[node] = D1[kvp.Key] + kvp.Value.Cost;
                            P[node] = kvp.Key;
                        }
                    });

            }

            List<Road> roads = new List<Road>();
            Node currentNode = endingNode;
            while (currentNode != startingNode)
            {
                if (currentNode == null) return null;
                if (!P.ContainsKey(currentNode)) return null;
                if (P[currentNode] == null) return null;
                if (!currentNode.Roads.ContainsKey(P[currentNode])) return null;
                roads.Add(currentNode.Roads[P[currentNode]]);
                currentNode = P[currentNode];
            }

            return roads;
        }

        public static List<Road> FloydWarshallAlgorithm(Graph graph, Node startingNode, Node endingNode)
        {
            Dictionary<Tuple<Node, Node>, double> D = new Dictionary<Tuple<Node, Node>, double>();
            Dictionary<Tuple<Node, Node>, Node> P = new Dictionary<Tuple<Node, Node>, Node>();

            graph.Nodes.Values.ToList().ForEach(i =>
            {
                graph.Nodes.Values.ToList().ForEach(j =>
                {
                    P.Add(new Tuple<Node, Node>(i, j), null);
                    if (i == j)
                    {
                        D.Add(new Tuple<Node, Node>(i, j), 0);
                    }
                    else
                    {
                        List<Road> r = graph.Roads.Where(road => road.StartingNode == i && road.EndingNode == j)
                            .ToList();
                        if (r.Any()) P[new Tuple<Node, Node>(i, j)] = i;
                        D.Add(new Tuple<Node, Node>(i, j), r.Any() ? r.First().Cost : double.MaxValue);
                    }
                });
            });

            graph.Nodes.Values.ToList().ForEach(k =>
            {
                graph.Nodes.Values.ToList().ForEach(i =>
                {
                    graph.Nodes.Values.ToList().ForEach(j =>
                    {

                        //IF dik +dkj < dij
                        //THEN BEGIN dij:= dik + dkj; pij:= pkj; END;
                        if (D[new Tuple<Node, Node>(i, k)] + D[new Tuple<Node, Node>(k, j)] <
                            D[new Tuple<Node, Node>(i, j)])
                        {
                            D[new Tuple<Node, Node>(i, j)] =
                                D[new Tuple<Node, Node>(i, k)] + D[new Tuple<Node, Node>(k, j)];
                            P[new Tuple<Node, Node>(i, j)] = P[new Tuple<Node, Node>(k, j)];
                        }

                    });
                });
            });

            Node currentNode = endingNode;
            List<Road> roads = new List<Road>();
            while (currentNode != startingNode)
            {
                if (currentNode == null) return null;
                if (!P.ContainsKey(new Tuple<Node, Node>(startingNode, currentNode))) return null;
                if (P[new Tuple<Node, Node>(startingNode, currentNode)] == null) return null;
                if (!currentNode.Roads.ContainsKey(P[new Tuple<Node, Node>(startingNode, currentNode)])) return null;
                roads.Add(currentNode.Roads[P[new Tuple<Node, Node>(startingNode, currentNode)]]);
                currentNode = P[new Tuple<Node, Node>(startingNode, currentNode)];
            }

            return roads;
        }

        private static bool DictionaryEqualityCheck(Dictionary<Node, double> dict, Dictionary<Node, double> dict2)
        {
            bool equal = false;
            if (dict.Count != dict2.Count) return false;
            equal = true;
            foreach (var pair in dict)
            {
                if (dict2.TryGetValue(pair.Key, out double value))
                {
                    // Require value be equal.
                    if (value != pair.Value)
                    {
                        equal = false;
                        break;
                    }
                }
                else
                {
                    // Require key be present.
                    equal = false;
                    break;
                }
            }
            return equal;
        }

        #endregion

        public static List<Road> EulerianCicleAlgorithm(Graph graph ,Node s)
        {
            List<Node> W = new List<Node>{s};
            Node x = s;
            List<Road> A = new List<Road>();
            List<Road> A1 = graph.Roads.ToList();
            
            List<Node> V = x.Roads.Where(key => A1.Any(a => a.Equals(key.Value))).ToDictionary(k => k.Key, k => k.Value).Keys.ToList();
            while (V.Any())
            {
                Node y;
                if (V.Count > 1)
                {
                    List<Node> filtered = V.Where(v => ExistsIndirectRoad(A1, graph.Nodes[x.NodeIndex].Roads[v])).ToList();
                    if (!filtered.Any()) y = V.First();
                    else y = filtered.First();

                }
                else y = V.First();     
                if (!A1.Contains(graph.Nodes[x.NodeIndex].Roads[y])) return null;
                Road r = graph.Nodes[x.NodeIndex].Roads[y];
                A.Add(r);
                A1.Remove(r);
                x = y;
                W.Add(x);
                V = x.Roads.Where(key => A1.Any(a => a.Equals(key.Value))).ToDictionary(k => k.Key, k => k.Value).Keys.ToList();
            }

            return A;
        }
    }
}