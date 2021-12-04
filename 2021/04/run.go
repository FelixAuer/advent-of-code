package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

type Square struct {
	Value  int
	Marked bool
}

type Bingo struct {
	Squares [5][5]Square
}

func (bingo Bingo) isWon() bool {
	for i := 0; i < 5; i++ {
		won := true
		for j := 0; j < 5; j++ {
			won = won && bingo.Squares[i][j].Marked

		}
		if won {
			return true
		}
	}
	for j := 0; j < 5; j++ {
		won := true
		for i := 0; i < 5; i++ {
			won = won && bingo.Squares[i][j].Marked
		}
		if won {
			return true
		}
	}
	/*won := true
	for i := 0; i < 5; i++ {
		won = won && bingo.Squares[i][i].Marked
	}
	if won {
		return true
	}

	won = true
	for i := 0; i < 5; i++ {
		won = won && bingo.Squares[4-i][i].Marked
	}
	if won {
		return true
	}*/

	return false
}

func (bingo Bingo) markNumber(number int) Bingo {
	for i := 0; i < 5; i++ {
		for j := 0; j < 5; j++ {
			if bingo.Squares[i][j].Value == number {
				bingo.Squares[i][j].Marked = true
			}
		}
	}

	return bingo
}

func (bingo Bingo) score() int {
	sumUnmarked := 0
	for i := 0; i < 5; i++ {
		for j := 0; j < 5; j++ {
			if !bingo.Squares[i][j].Marked {
				sumUnmarked += bingo.Squares[i][j].Value
			}
		}
	}

	return sumUnmarked
}

func main() {
	lines := readInput()
	fmt.Println(partOne(lines))
	fmt.Println(partTwo(lines))

}

func partOne(lines []string) int {
	calledNumbers := strings.Split(lines[0], ",")
	var bingos []Bingo

	bingoIndex := 2
	for bingoIndex < len(lines) {
		bingos = append(bingos, createBingo(lines[bingoIndex:bingoIndex+5]))
		bingoIndex += 6
	}

	for _, number := range calledNumbers {
		val, _ := strconv.Atoi(number)
		for i, bingo := range bingos {
			bingo = bingo.markNumber(val)
			if bingo.isWon() {
				return bingo.score() * val
			}
			bingos[i] = bingo
		}
	}
	return 0
}

func partTwo(lines []string) int {
	calledNumbers := strings.Split(lines[0], ",")
	var bingos []Bingo

	bingoIndex := 2
	for bingoIndex < len(lines) {
		bingos = append(bingos, createBingo(lines[bingoIndex:bingoIndex+5]))
		bingoIndex += 6
	}

	for _, number := range calledNumbers {
		val, _ := strconv.Atoi(number)
		var unwonBingos []Bingo
		for _, bingo := range bingos {
			bingo = bingo.markNumber(val)
			if !bingo.isWon() {
				unwonBingos = append(unwonBingos, bingo)
			}
		}
		if len(bingos) == 1 && len(unwonBingos) == 0 {
			return val * bingos[0].markNumber(val).score()
		}
		bingos = unwonBingos
	}
	return 0
}

func remove(slice []int, s int) []int {
	return append(slice[:s], slice[s+1:]...)
}

func createBingo(lines []string) Bingo {
	var squares [5][5]Square
	for i, line := range lines {
		for j, val := range strings.Fields(line) {
			ival, _ := strconv.Atoi(val)
			squares[i][j] = Square{ival, false}
		}
	}

	return Bingo{squares}
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
