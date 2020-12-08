INPUT_FILE = "input.txt"


# Christmas Processing Unit
class CPU:

    def __init__(self, instructions, show_errors=False):
        self.accumulator = 0
        self.current_line = 0
        self.instructions = [{"instruction": instruction, "finished": False} for instruction in instructions]
        self.finished = False
        self.show_errors = show_errors

    def run(self):
        running = True

        try:
            while running:
                running = self.step()

            self.finished = True
        except RuntimeError as re:
            if self.show_errors:
                print(re)

        return self.accumulator

    def step(self):

        if self.current_line >= len(self.instructions):
            return False

        instruction = self.instructions[self.current_line]

        if instruction["finished"]:
            raise RuntimeError("Infinite Loop Detected")

        [operation, value] = instruction["instruction"].split()
        value = int(value)

        getattr(self, operation)(value)
        instruction["finished"] = True

        return True

    def acc(self, value):
        self.accumulator = self.accumulator + value
        self.current_line = self.current_line + 1

    def jmp(self, value):
        self.current_line = self.current_line + value

    def nop(self, value):
        self.current_line = self.current_line + 1


def main():
    instructions = load_input()

    part_one(instructions)
    part_two(instructions)


def part_one(instructions):
    cpu = CPU(instructions, show_errors=True)
    cpu.run()
    print(cpu.accumulator)


def part_two(instructions):
    for i in range(len(instructions)):
        if instructions[i].startswith("nop") or instructions[i].startswith("jmp"):
            copied = instructions[:]
            if copied[i].startswith("nop"):
                copied[i] = copied[i].replace("nop", "jmp")
            else:
                copied[i] = copied[i].replace("jmp", "nop")

            cpu = CPU(copied)
            cpu.run()
            if cpu.finished:
                print(cpu.accumulator)


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
