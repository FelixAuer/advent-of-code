import re
import math

INPUT_FILE = "input.txt"


def main():
    lines = load_input()
    part_one(lines)
    part_two(lines)


def part_one(lines):
    print(sum([eval_full(exp, eval_basic_1) for exp in lines]))


def part_two(lines):
    print(sum([eval_full(exp, eval_basic_2) for exp in lines]))


def eval_basic_1(expression):
    terms = expression.split()
    terms.reverse()

    result = int(terms.pop())

    while len(terms) > 0:
        operator = terms.pop()
        value = int(terms.pop())

        if operator == '+':
            result += value
        if operator == '*':
            result *= value

    return result


def eval_basic_2(expression):
    addition_term = re.search(r"\d+ \+ \d+", expression)
    while addition_term:
        addition_expression = addition_term[0]
        sum_term = sum(map(int, addition_expression.split(" + ")))
        expression = expression[:addition_term.start()] + str(sum_term) + expression[addition_term.end():]
        addition_term = re.search(r"\d+ \+ \d+", expression)

    numbers = list(map(int, expression.split(" * ")))
    return math.prod(numbers)


def eval_full(expression, eval_func):
    inner_term = re.search(r"\([^\(\)]+\)", expression)
    while inner_term:
        inner_expression = inner_term[0]
        value = eval_func(inner_expression[1:-1])
        expression = expression.replace(inner_expression, str(value))
        inner_term = re.search(r"\([^\(\)]+\)", expression)
    return eval_func(expression)


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
