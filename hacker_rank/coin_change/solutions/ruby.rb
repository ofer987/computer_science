#!/bin/ruby

n, m = gets.strip.split(' ').map(&:to_i)
coins = gets.strip.split(' ').map(&:to_i)

class Memoization < Hash
  def initialize

  end

  def check_for(value)
    if fetch(value) do
     
    end
  end

  def valid?
    
  end
end

class Tree
  def initialize(coins, value, memo)
    # sort in descending order
    @coins = coins.sort { |x, y| x <=> y }
    @value = value
    @memo = memo
  end

  def use_coin
    children = @coins.map do |coin|
      new_value = @value - coin
      if (new_value >= 0)
        @memo.check_for(new_value)
        Tree.new(@coins, new_value)
      end
    end
  end

  private

  def valid?
    if @memo.has_key?(@value)
      @memo[@value]
    else
    end
  end
end
