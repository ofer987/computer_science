# Only 20-bit unsigned values can be stored on the stack
class LifoStack
  MIN_VALUE = 0
  MAX_VALUE = 2**20-1

  attr_reader :internal, :index

  def initialize
    # stack is empty
    @internal = []
    # pointer to latest element
    @index = -1
  end

  def append(value)
    raise "value cannot be negative" if value < MIN_VALUE
    raise "value cannot be greater than #{MAX_VALUE}" if value > MAX_VALUE

    if @index + 1 < internal.length
      @internal[@index+1] = value
    else
      @internal << value
    end

    @index += 1
  end
  alias_method :<<, :append

  def add
    assert_not_empty
    assert_two_items

    append(pop + pop)
  end

  def sub
    assert_not_empty
    assert_two_items

    append(pop - pop)
  end

  def pop
    assert_not_empty

    @index -= 1
    @internal[@index+1]
  end

  def peek
    assert_not_empty

    @internal[@index]
  end

  def dup
    assert_not_empty

    append(peek)
  end

  private

  def assert_not_empty
    raise "stack is empty" if @index < 0
  end

  def assert_two_items
    raise "at least two items required" if @index < 1
  end
end

class Controller
  attr_reader :stack

  def initialize
    @stack = LifoStack.new
  end

  # return last value or -1 (if error)
  def perform_all(operations)
    begin
      Array(operations).each do |operation|
        perform(operation)
      end

      # return last value
      stack.peek
    rescue => e
      # if error then return -1
      -1
    end
  end

  def perform(operation)
    op = operation.to_s.upcase
    case op
    when 'POP'
      stack.pop
    when 'DUP'
      stack.dup
    when '+'
      stack.add
    when '-'
      stack.sub
    else
      # value should be an integer
      # if operation is not an integer than raise an error
      value = Integer(op)
      stack << value
    end
  end
end

def solution(s)
  operations = s.to_s.strip.split(' ')

  Controller.new.perform_all(operations)
end
