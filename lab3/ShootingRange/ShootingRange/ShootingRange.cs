public interface IShootingRangeObserver
{
    void windSpeedChanged();

    void windDirectionChanged();

    void projectileSpeedChanged();

    void targetMoved();

    void targetHit();

    void shotMissed();
}

namespace ShootingRange
{
    public enum WindDirection
    {
        NORTH,
        SOUTH,
        WEST,
        EAST
    }

    public class ShootingRangeObservable
    {
        private List<IShootingRangeObserver> subscribers = new List<IShootingRangeObserver>();
        private uint distanceToTarget = 10;
        private uint projectileSpeed = 10;
        private uint windSpeed = 10;
        private WindDirection windDirection = WindDirection.NORTH;

        public void notifySubscribersWindSpeedChanged()
        {
            subscribers.ForEach(observer => observer.windSpeedChanged());
        }

        public void notifySubscribersTargetMoved()
        {
            subscribers.ForEach(observer => observer.targetMoved());
        }

        public void notifySubscribersProjectileSpeedChanged()
        {
            subscribers.ForEach(observer => observer.projectileSpeedChanged());
        }

        public void notifySubscribersTargetHit()
        {
            subscribers.ForEach(observer => observer.targetHit());
        }

        public void notifySubscribersShotMissed()
        {
            subscribers.ForEach(observer => observer.shotMissed());
        }

        public void notifySubscribersWindDirectionChanged()
        {
            subscribers.ForEach(observer => observer.windDirectionChanged());
        }

        public void setWindSpeed(uint windSpeed)
        {
            if (this.windSpeed!= windSpeed)
            {
                this.windSpeed = windSpeed;
                notifySubscribersWindSpeedChanged();
            }
        }

        public void setProjectileSpeed(uint projectileSpeed)
        {
            if (this.projectileSpeed != projectileSpeed)
            {
                this.projectileSpeed = projectileSpeed;
                notifySubscribersProjectileSpeedChanged();
            }
        }

        public void setDistanceToTarget(uint distanceToTarget)
        {
            if (this.distanceToTarget != distanceToTarget)
            {
                this.distanceToTarget = distanceToTarget;
                notifySubscribersTargetMoved();
            }
        }

        public void setWindDirection(WindDirection windDirection)
        {
            if (this.windDirection != windDirection)
            {
                this.windDirection = windDirection;
                notifySubscribersWindDirectionChanged();
            }
        }

        public Int32 subscribe(IShootingRangeObserver observer)
        {
            subscribers.Add(observer);

            return subscribers.Count - 1;
        }

        public void unsubscribe(Int32 index)
        {
            subscribers.RemoveAt(index);
        }

        public void shoot(long correction)
        {
            uint flyingTime = distanceToTarget / projectileSpeed;

            if (windDirection == WindDirection.NORTH || windDirection == WindDirection.SOUTH)
            {
                if (correction == 0)
                {
                    notifySubscribersTargetHit();
                }
                else
                {
                    notifySubscribersShotMissed();
                }
                return;
            }

            long directWindSpeed = windDirection == WindDirection.WEST
                ? -windSpeed
                : windSpeed;

            if (directWindSpeed * flyingTime == correction)
            {
                notifySubscribersTargetHit();
            }
            else
            {
                notifySubscribersShotMissed();
            }
        }
    }
}