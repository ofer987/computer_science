#!/usr/bin/env ruby

class Parentheses
  OPENINGS = [ '(', '[', '{' ]
  CLOSINGS = [ ')', ']', '}' ]

  attr_reader :str

  def initialize(str)
    @str = str
  end

  def is_closed?
    stack = []

    str.each_char do |ch|
      if is_opening?(ch)
        stack.push(ch)
      elsif is_closing?(ch)
        if stack[-1] == opening(ch)
          # pop the last element
          stack.pop
        else
          return false
        end
      else
        # ignore non-parenthesis characters
      end
    end

    # stack should be empty if all elements have been matched (i.e., popped)
    stack.empty?
  end

  private

  def opening(closing)
    CLOSINGS
      .find_index(closing)
      .tap { |index| return OPENINGS[index] }
  end

  def is_opening?(ch)
    not OPENINGS.find { |opening| ch == opening }.nil?
  end

  def is_closing?(ch)
    not CLOSINGS.find { |closing| ch == closing }.nil?
  end
end

class Program
  attr_reader :input

  def main
    puts Parentheses.new(gets).is_closed?.to_s.capitalize
  end
end

# Execute
Program.new.main()
