#!/usr/bin/env ruby

def solution(stones, max_distance)
  stations = StationsBuilder.new(stones).to_a

  Pond.new(stations, max_distance).earliest_time
end

class StationsBuilder
  attr_reader :stones

  def initialize(stones)
    @stones = stones
  end

  def to_a
    stations = []

    stations << StartShore.new

    stones.each_with_index do |time_required, index|
      stations << Stone.new(time_required, index + 1)
    end

    stations << EndShore.new(stones.length + 1)

    stations
  end
end

class Stone
  attr_reader :time_required, :location

  def initialize(time_required, location)
    @time_required = time_required
    @location = location
  end

  def valid?(time)
    time_required != -1 && time_required <= time
  end

  def finished?
    false
  end
end

class StartShore
  def time_required
    0
  end

  def location
    0
  end

  def valid?(time)
    true
  end

  def finished?
    false
  end
end

class EndShore
  attr_reader :location

  def initialize(location)
    @location = location
  end

  def time_required
    0
  end

  def valid?(time)
    true
  end

  def finished?
    true
  end
end

class Pond
  attr_reader :stations, :max_distance

  def initialize(stations, max_distance)
    @stations = stations
    @max_distance = max_distance
  end

  def earliest_time
    times = sorted_times

    times.each do |time|
      return time if can_jump_to_end?(stations.first, time)
    end

    return -1
  end

  private

  def can_jump_to_end?(station, time)
    return true if station.finished?
    return false unless station.valid?(time)

    next_stations(station, max_distance).reverse.each do |s|
      return true if can_jump_to_end?(s, time)
    end

    return false
  end

  # Return the earliest time that the frog can leap all the stations
  # Retuns -1 if it is impossible
  def sorted_times
    stations
      .map { |station| station.time_required }
      .sort_by { |time| time }
  end

  def next_stations(start, distance)
    start_location = start.location + 1
    end_location = start.location + distance

    stations[start_location..end_location]
  end
end
