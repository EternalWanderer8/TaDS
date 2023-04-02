using ShootingRange;
using Moq;

namespace ShootingRangeTests
{
    public class ShootingRangeTests
    {
        ShootingRangeObservable shootingRange = new ShootingRangeObservable();

        Mock<IShootingRangeObserver> observerMock = new Mock<IShootingRangeObserver>();

        [Fact]
        public void Change_wind_speed_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindSpeed(100);

            observerMock.Verify(observer => observer.windSpeedChanged(), Times.Once());
        }

        [Fact]
        public void Change_wind_speed_observer_not_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindSpeed(10);

            observerMock.Verify(observer => observer.windSpeedChanged(), Times.Never());
        }

        [Fact]
        public void Change_wind_direction_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.EAST);

            observerMock.Verify(observer => observer.windDirectionChanged(), Times.Once());
        }

        [Fact]
        public void Change_wind_direction_observer_not_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.NORTH);

            observerMock.Verify(observer => observer.windDirectionChanged(), Times.Never());
        }

        [Fact]
        public void Change_projectile_speed_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setProjectileSpeed(100);

            observerMock.Verify(observer => observer.projectileSpeedChanged(), Times.Once());
        }

        [Fact]
        public void Change_projectile_speed_observer_not_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setProjectileSpeed(10);

            observerMock.Verify(observer => observer.projectileSpeedChanged(), Times.Never());
        }

        [Fact]
        public void Change_target_move_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setDistanceToTarget(100);

            observerMock.Verify(observer => observer.targetMoved(), Times.Once());
        }

        [Fact]
        public void Change_target_move_observer_not_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setDistanceToTarget(10);

            observerMock.Verify(observer => observer.targetMoved(), Times.Never());
        }

        [Fact]
        public void Shoot_with_north_wind_direction_target_hit_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.shoot(0);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_south_wind_direction_target_hit_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.SOUTH);
            shootingRange.shoot(0);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_south_wind_direction_shot_missed_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.SOUTH);
            shootingRange.shoot(10);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Shoot_with_north_wind_direction_shot_missed_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.SOUTH);
            shootingRange.shoot(-10);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Shoot_with_east_wind_direction_target_hit_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.EAST);
            shootingRange.shoot(10);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_east_wind_direction_shot_missed_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.EAST);
            shootingRange.shoot(100);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Shoot_with_west_wind_direction_target_hit_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.WEST);
            shootingRange.shoot(-10);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_west_wind_direction_shot_missed_observer_notified()
        {
            shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.WEST);
            shootingRange.shoot(100);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Change_wind_speed_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);
            shootingRange.setWindSpeed(100);
            shootingRange.unsubscribe(index);
            shootingRange.setWindSpeed(120);

            observerMock.Verify(observer => observer.windSpeedChanged(), Times.Once());
        }

        [Fact]
        public void Change_wind_direction_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);
            shootingRange.setWindDirection(WindDirection.EAST);
            shootingRange.unsubscribe(index);
            shootingRange.setWindDirection(WindDirection.SOUTH);

            observerMock.Verify(observer => observer.windDirectionChanged(), Times.Once());
        }

        [Fact]
        public void Change_projectile_speed_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);
            shootingRange.setProjectileSpeed(100);
            shootingRange.unsubscribe(index);
            shootingRange.setProjectileSpeed(120);

            observerMock.Verify(observer => observer.projectileSpeedChanged(), Times.Once());
        }

        [Fact]
        public void Change_target_moved_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);
            shootingRange.setDistanceToTarget(100);
            shootingRange.unsubscribe(index);
            shootingRange.setDistanceToTarget(120);

            observerMock.Verify(observer => observer.targetMoved(), Times.Once());
        }

        [Fact]
        public void Shoot_with_north_wind_direction_target_hit_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.shoot(0);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(0);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_north_wind_direction_shot_missed_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.shoot(10);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(10);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Shoot_with_south_wind_direction_target_hit_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.SOUTH);
            shootingRange.shoot(0);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(0);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_south_wind_direction_shot_missed_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.SOUTH);
            shootingRange.shoot(10);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(10);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Shoot_with_east_wind_direction_shot_missed_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.EAST);
            shootingRange.shoot(100);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(100);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }

        [Fact]
        public void Shoot_with_east_wind_direction_target_hit_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.EAST);
            shootingRange.shoot(10);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(10);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_west_wind_direction_target_hit_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.WEST);
            shootingRange.shoot(-10);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(-10);

            observerMock.Verify(observer => observer.targetHit(), Times.Once());
            observerMock.Verify(observer => observer.shotMissed(), Times.Never());
        }

        [Fact]
        public void Shoot_with_west_wind_direction_shot_missed_unsubscribed_observer_not_notified()
        {
            var index = shootingRange.subscribe(observerMock.Object);

            shootingRange.setWindDirection(WindDirection.WEST);
            shootingRange.shoot(100);
            shootingRange.unsubscribe(index);
            shootingRange.shoot(100);

            observerMock.Verify(observer => observer.targetHit(), Times.Never());
            observerMock.Verify(observer => observer.shotMissed(), Times.Once());
        }
    }
}
