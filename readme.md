# Solving joinstrings Kattis problem

Project for solving, testing and submitting Kattis solutions

## Dependencies

Generator requires:

* dotnet
* npm
* python
  * requests library `pip3 install requests`
* yeoman `npm install -g yo`

## Quick start

1. Get your `.kattisrc` file from Kattis [https://[ContestNAME].kattis.com/download/kattisrc](https://[ContestNAME].kattis.com/download/kattisrc)
2. Save it in home directory created for your contest e.g. `/Users/piotr.karpala/Projects/github/kattis/.kattisrc`
3. Create directory for your problem `mkdir -p /Users/piotr.karpala/Projects/github/kattis/joinstrings`
4. Go to directory `cd /Users/piotr.karpala/Projects/github/kattis/joinstrings`
5. Run yeoman `yo kattisnet`
6. Run tests `npm run test` while you solve the problem
   1. [Optional] run format `npm run format`
7. Submit your solution `npm run submit`

## Solving the solution

Go to `Tests` directory and run `dotnet watch tests` while you solve the problem.

## Submit

Use automated submit process `npm run submit` or submit following files to Kattis:

* `InputOutput.cs`
* `Program.cs`
