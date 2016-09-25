#!/usr/bin/env ruby

class TestCases
  attr_reader :dir

  def initialize(dir)
    @dir = dir
  end

  def each
    begin
      test_files.each do |file|
        lines = IO.readlines(file)

        # should be two lines per case:
        # 1. arguments
        # 2. result
        length = lines.length
        (0...length).step(2).each do |index|
          arguments = lines[index].strip
          result = eval(lines[index+1].strip)

          result == yield(arguments, result)
        end
      end
    rescue => e
      puts e
    end
  end

  private

  def test_dir
    @test_dir ||= File.join(dir, 'test_cases')
  end

  def test_files
    @test_files ||= Dir.glob("#{test_dir}/*")
  end

  def parse_to_objects
  end
end

class Runner
  attr_reader :dir

  def initialize(dir)
    @dir = dir
  end

  def run(filename = FILE_NAME)
    require File.join(dir, filename)

    test_cases.each do |args, expected_output|
      begin
        puts "For arguments #{args} (expected output is #{expected_output})"
        actual_output = eval("solution(#{args})")
        if (expected_output == actual_output)
          puts "Success"
        else
          puts "Fail: got #{actual_output}"
        end
      rescue => e
        puts e
      end
    end
  end

  private

  def test_cases
    @test_cases ||= TestCases.new(dir)
  end
end

FILE_NAME = 'solution.rb'.freeze

filename = ARGV[0] || FILE_NAME
Runner.new(Dir.pwd).run(filename)
