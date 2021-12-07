package main

import (
	"bufio"
	"fmt"
	"log"
	"math"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	line := readInput()[0]
	numberStrings := strings.Split(line, ",")
	var numbers []int
	for _, number := range numberStrings {
		val, _ := strconv.Atoi(number)
		numbers = append(numbers, val)
	}
	sort.Ints(numbers)

	fmt.Println(partOne(numbers))
	fmt.Println(partTwo(numbers))
}

func partOne(numbers []int) int {
	median := calculateMedian(numbers)

	sumFuel := 0
	for _, val := range numbers {
		sumFuel += int(math.Abs(float64(val - median)))
	}

	return sumFuel
}
func partTwo(numbers []int) int {
	lower, upper := numbers[0], numbers[len(numbers)-1]
	minFuel := math.MaxInt

	for meetingPoint := lower; meetingPoint <= upper; meetingPoint++ {
		sumFuel := 0
		for _, val := range numbers {
			distance := int(math.Abs(float64(val - meetingPoint)))
			sumFuel += distance * (distance + 1) / 2
		}

		if sumFuel < minFuel {
			minFuel = sumFuel
		}
	}
	return minFuel
}

func calculateMedian(numbers []int) int {
	length := len(numbers)

	if length%2 == 0 {
		return (numbers[length/2] + numbers[(length+1)/2]) / 2
	}

	return numbers[(length+1)/2]
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
