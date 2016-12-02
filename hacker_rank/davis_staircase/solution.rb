#!/bin/ruby

class Memory
  def initialize(size)
    @values = Array.new(size)
  end

  def [](index)
    @values[index]
  end

  def has_key?(index)
    index < @values.size && !@values[index].nil?
  end

  def append(index, count)
    @values[index] = count
  end
end

class NilNode
  def nil?
    true
  end

  def leaf?
    false
  end

  def remaining
    0
  end

  def leaves_count
    0
  end

  def compute_nodes
    puts "Compute for NilNode"
  end
end

class Leaf
  def initialize(memory)
    @memory = memory
  end

  def nil?
    false
  end

  def leaves_count
    1
  end

  def compute_nodes
    puts "Compute for Leaf"
  end

  def remaining
    0
  end

  def leaf?
    true
  end
end

class Node
  attr_reader :remaining, :leaves_count

  def initialize(remaining, memory)
    @remaining = remaining
    @memory = memory
    @leaves_count = nil
  end

  def nil?
    false
  end

  def leaf?
    false
  end

  def compute_nodes
    puts "Does memory have key for #{@remaining}?"
    return if @memory.has_key?(@remaining)
    puts "False"

    # left
    puts "Left"
    one.compute_nodes

    # right
    puts "First Right"
    two.compute_nodes

    # right
    puts "Second Right"
    three.compute_nodes

    # self
    puts "Self"
    puts "1) Remaining #{@remaining} has #{@leaves_count} leaves"
    puts to_s

    @leaves_count = [one, two, three]
      .map { |node| puts "Node (#{node.class.to_s}) (#{node.remaining}) is nil" if node.leaves_count.nil?; node.leaves_count.to_i }
      .reduce { |count, sum| sum += count }

    puts "2) Remaining #{@remaining} has #{@leaves_count} leaves"
    puts

    @memory.append(@remaining, @leaves_count)
  end

  def nodes
    return self if leaf?

    [one, two, three]
      .select { |node| !node.nil? }
      .flat_map { |node| node.nodes }
  end

  def one
    @one ||= child(@remaining - 1)
  end

  def two
    @two ||= child(@remaining - 2)
  end

  def three
    @three ||= child(@remaining - 3)
  end

  def to_s
    @remaining
  end

  private

  def child(remaining)
    if remaining > 0
      Node.new(remaining, @memory)
    elsif remaining == 0
      Leaf.new(@memory)
    else
      NilNode.new
    end
  end
end

class Tree
  attr_reader :steps

  def initialize(steps, memory)
    @steps = steps
    @memory = memory
  end

  def compute_nodes
    root.compute_nodes
  end

  def leaves
    root.leaves_count
  end

  def root
    Node.new(@steps, @memory)
  end
end

def count(steps)
  memory = Memory.new(steps)

  tree = Tree.new(steps, memory)
  tree.compute_nodes

  tree.leaves
end

def main
  s = gets.strip.to_i
  for a0 in (0..s-1)
    n = gets.strip.to_i

    puts "Staircase of size = #{n}"
    count = count(n)
    puts "Answer: #{count}"
  end
end

main
