﻿using AnnMaze;

class Program
{

    static void Main(string[] args)
    {
        //Initialize Neural Network
        var curNeuralNetwork = new NeuralNetwork(1, 6);

                                                                 //Console.WriteLine("Synaptic weights before training:");
                                                                 //PrintMatrix(curNeuralNetwork.SynapsesMatrix);

        //Train Network
        var trainingInputs = new double[,] { { 1, 1, 1, 1, 1, 1 }, { 0, 0, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0, 0 }, { 0, 0, 1, 1, 1, 1 }, { 1, 0, 0, 1, 1, 0 }, { 0, 0, 1, 0, 1, 1 } };
        var trainingOutputs = NeuralNetwork.MatrixTranspose(new double[,] { { 1, 0, 1, 0, 1, 0 } });

        curNeuralNetwork.Train(trainingInputs, trainingOutputs, 10000);

                                                                 //Console.WriteLine("\nSynaptic weights after training:");
                                                                 //PrintMatrix(curNeuralNetwork.SynapsesMatrix);
        //Start game
        Console.WriteLine(" ");
        Game currentGame = new Game(curNeuralNetwork);
        currentGame.Start();
    }

    static void PrintMatrix(double[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", matrix[i, j]));
            }
            Console.Write(Environment.NewLine);
        }
    }
}