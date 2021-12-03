package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
)

func main() {
	lines := readInput()
	gammaRates := toGammaRates(lines)
	fmt.Println(partOne(gammaRates))
	fmt.Println(partTwo(gammaRates))
}

func partOne(gammaRates [][]int) int {
	mostCommon, leastCommon := getCommons(gammaRates)
	return gammaToNumber(mostCommon) * gammaToNumber(leastCommon)
}

func partTwo(gammaRates [][]int) int {
	oxygen, scrubber := filterCommonInPosition(gammaRates)
	return oxygen * scrubber
}

func filterCommonInPosition(gammaRates [][]int) (int, int) {
	mostCommon, _ := getCommons(gammaRates)
	position := 0
	matchesMostCommon := filterMatches(gammaRates, mostCommon, position)
	for len(matchesMostCommon) > 1 {
		position++
		mostCommon, _ := getCommons(matchesMostCommon)
		matchesMostCommon = filterMatches(matchesMostCommon, mostCommon, position)
	}

	_, leastCommon := getCommons(gammaRates)
	position = 0
	matchesLeastCommon := filterMatches(gammaRates, leastCommon, position)
	for len(matchesLeastCommon) > 1 {
		position++
		_, leastCommon := getCommons(matchesLeastCommon)
		matchesLeastCommon = filterMatches(matchesLeastCommon, leastCommon, position)
	}
	return gammaToNumber(matchesMostCommon[0]), gammaToNumber(matchesLeastCommon[0])
}

func filterMatches(gammaRates [][]int, mostCommon []int, position int) [][]int {
	var matches [][]int
	for _, gammaRate := range gammaRates {
		if gammaRate[position] == mostCommon[position] {
			matches = append(matches, gammaRate)
		}
	}
	return matches
}

func getCommons(gammaRates [][]int) ([]int, []int) {
	length := float32(len(gammaRates))

	sums := make([]float32, len(gammaRates[0]))

	for _, gammaRate := range gammaRates {
		for i, val := range gammaRate {
			sums[i] += float32(val)

		}
	}

	mostCommon := make([]int, len(gammaRates[0]))
	leastCommon := make([]int, len(gammaRates[0]))
	for i, val := range sums {
		if (val - float32(length/2)) >= 0 {
			mostCommon[i] = 1
			leastCommon[i] = 0
		} else {
			leastCommon[i] = 1
		}
	}
	return mostCommon, leastCommon
}

func gammaToNumber(reading []int) int {
	stringFormat := ""
	for _, val := range reading {
		stringFormat += strconv.Itoa(val)
	}
	number, _ := strconv.ParseInt(stringFormat, 2, 64)
	return int(number)
}

func toGammaRates(lines []string) [][]int {
	var gammaRates [][]int
	for _, line := range lines {
		gammaRates = append(gammaRates, stringToIntArray(line))
	}
	return gammaRates
}

func stringToIntArray(reading string) []int {
	var ints []int

	for _, char := range reading {
		ints = append(ints, int(char-'0'))
	}

	return ints
}

func readInput() []string {
	file, err := os.Open("input.txt")

	if err != nil {
		log.Fatalf("failed to open")

	}

	// The bufio.NewScanner() function is called in which the
	// object os.File passed as its parameter and this returns a
	// object bufio.Scanner which is further used on the
	// bufio.Scanner.Split() method.
	scanner := bufio.NewScanner(file)

	// The bufio.ScanLines is used as an
	// input to the method bufio.Scanner.Split()
	// and then the scanning forwards to each
	// new line using the bufio.Scanner.Scan()
	// method.
	scanner.Split(bufio.ScanLines)
	var lines []string

	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	// The method os.File.Close() is called
	// on the os.File object to close the file
	file.Close()

	return lines
}
