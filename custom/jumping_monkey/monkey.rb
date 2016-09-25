#!/usr/bin/env ruby

def solution(array, max_distance)
  stones = StoneBuilder.new(array).to_a

  Pond.new(stones, max_distance).earliest_time
end

class StoneBuilder
  def initialize(array)
    @array = array
  end

  def to_a
    # the starting shore + The stones + the end shore (is always ready)
    (0 + array + 0).map do |time_required|
      Stone.new(time_required)
    end
  end
end

class Stones
  attr_reader :items

  def initialize(items)
    @item = item
  end

  def to_a
    # The stones + the shore (is always ready)
    (array + 0).map do |time_required|
      Stone.new(time_required)
    end
  end

  def times
    (array + 0).select { |time| time >= 0 }
  end

  def valid(time)
    items.map do |item|
      item.valid?
    end
  end
end

class Stone
  attr_reader :time_required

  def initialize(time_required)
    @time_required = time_required
  end

  def valid?(time)
    time_required != -1 && time_required <= time
  end
end

class Pond
  attr_reader :stones, :max_distance

  def initialize(stones, max_distance)
    @stones = stones
    @max_distance = max_distance
  end

  # Return the earliest time that the frog can leap all the stones
  # Retuns -1 if it is impossible
  def earliest_time
    stones
      .times
      .sort_by { |time| time }
      .find(-1) { |time| can_jump?(time) }
  end

  def can_jump?(time)
    get_distances(stones.valid(time)).all? do |distance|
      distance <= max_distance
    end
  end

  private

  def get_distances(bool_array)
    # assume that the first item is true
    distance = 1
    bool_array[1..-1].each_with_object([]) do |is_true, result|
      if is_true
        result << distance
        distance = 1
      else
        distance += 1
      end
    end
  end
end
